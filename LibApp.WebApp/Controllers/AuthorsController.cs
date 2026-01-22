using AutoMapper;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LibApp.WebApp.Controllers;

//TODO: Remove LibraryContext from primary constructor in all controllers
[Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
public class AuthorsController(IAuthorService authorService, IUserService userService, IMapper mapper, UserManager<User> userManager) : Controller
{
    private const int PageSize = 10;
    private const string SortNameOrder = "name_desc";

    // GET: Authors
    public async Task<IActionResult> Index(string sortNameOrder, string currentNameFilter, string searchNameString,
        int? page)
    {
        ViewBag.CurrentSortName = sortNameOrder;
        ViewBag.SortNameParm = String.IsNullOrEmpty(sortNameOrder) ? SortNameOrder : "";

        if (searchNameString != null)
        {
            page = 1;
        }
        else
        {
            searchNameString = currentNameFilter;
        }

        ViewBag.CurrentNameFilter = searchNameString;

        try
        {
            var authors = await authorService.GetAuthorsAsync();
            var authorViewModels = mapper.Map<IEnumerable<AuthorViewModel>>(authors);

            if (!string.IsNullOrEmpty(searchNameString))
            {
                authorViewModels = authorViewModels.Where(a => a.Name.ToLower().Contains(searchNameString.ToLower()));
            }

            authorViewModels = sortNameOrder switch
            {
                SortNameOrder => authorViewModels.OrderByDescending(a => a.Name),
                _ => authorViewModels.OrderBy(a => a.Name)
            };

            var pageNumber = (page ?? 1);

            ViewBag.Authors = authorViewModels.ToPagedList(pageNumber, PageSize);

            return View();
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Authors/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var author = await authorService.GetAuthorAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorViewModel = mapper.Map<AuthorViewModel>(author);

            return View(authorViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Authors/Create
    public async Task<IActionResult> Create()
    {
        try
        {
            var usersAsEnumerable = await userService.GetUsersAsync();
            var users = usersAsEnumerable.ToList();
            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name");
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name");

            return View();
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Authors/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AuthorViewModel authorViewModel)
    {
        try
        {
            if (await authorService.AuthorExistsAsync(authorViewModel.Name))
            {
                ModelState.AddModelError("Name", "An author with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));

                var author = mapper.Map<Author>(
                    authorViewModel,
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = loggedInUserId;
                    });

                await authorService.AddAuthorAsync(author);
                
                TempData["SuccessMessage"] = "Author added successfully.";
                
                return RedirectToAction(nameof(Index));
            }

            var usersAsEnumerable = await userService.GetUsersAsync();
            var users = usersAsEnumerable.ToList();
            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", authorViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", authorViewModel.ModifiedByUserId);
            return View(authorViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Authors/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        try
        {
            var author = await authorService.GetAuthorAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorViewModel = mapper.Map<AuthorViewModel>(author);

            var usersAsEnumerable = await userService.GetUsersAsync();
            var users = usersAsEnumerable.ToList();
            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", author.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", author.ModifiedByUserId);
            return View(authorViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Authors/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AuthorViewModel authorViewModel)
    {
        if (id != authorViewModel.Id)
        {
            return NotFound();
        }

        try
        {
            if (await authorService.AuthorExistsInOtherAuthorsAsync(authorViewModel.Id, authorViewModel.Name))
            {
                ModelState.AddModelError("Name", "An author with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));
                var createdByUserId = authorViewModel.CreatedByUserId;

                var author = mapper.Map<Author>(
                    authorViewModel,
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = createdByUserId;
                    });

                await authorService.UpdateAuthorAsync(author);

                TempData["SuccessMessage"] = "Author updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            var usersAsEnumerable = await userService.GetUsersAsync();
            var users = usersAsEnumerable.ToList();
            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", authorViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", authorViewModel.ModifiedByUserId);
            return View(authorViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Authors/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var author = await authorService.GetAuthorAsync(id);
            if (author != null)
            {
                var isDeletable = await authorService.IsDeletableAsync(author.Id);
                if (isDeletable)
                {
                    await authorService.RemoveAuthorAsync(author);
                    TempData["SuccessMessage"] = "Author deleted successfully.";
                    return Json(new { success = true, message = "Author deleted successfully." });
                }
                    
                TempData["ErrorMessage"] = "Author cannot be deleted because it has associated books.";
                return Json(new { success = false, message = "Author cannot be deleted because it has associated books." });
            }

            TempData["ErrorMessage"] = "Author was not deleted. An error occurred while processing your request.";
            return Json(new { success = false, message = "Author not found." });
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }
}