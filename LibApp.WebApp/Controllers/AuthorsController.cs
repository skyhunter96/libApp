using AutoMapper;
using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibApp.WebApp.Controllers
{
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
    public class AuthorsController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IAuthorService _authorService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        private const int PageSize = 10;
        private const string SortNameOrder = "name_desc";

        public AuthorsController(LibraryContext context, IAuthorService authorService, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _authorService = authorService;
            _mapper = mapper;
            _userManager = userManager;
        }

        //TODO: Delete behavior with existing related entities - don't allow, alert - not possible cuz related?

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
                var authors = await _authorService.GetAuthorsAsync();
                var authorViewModels = _mapper.Map<IEnumerable<AuthorViewModel>>(authors);

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
                var author = await _authorService.GetAuthorAsync(id);

                if (author == null)
                {
                    return NotFound();
                }

                var authorViewModel = _mapper.Map<AuthorViewModel>(author);

                return View(authorViewModel);
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name");
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name");

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
                if (_authorService.AuthorExists(authorViewModel.Name))
                {
                    ModelState.AddModelError("Name", "An author with this Name already exists.");
                }

                if (ModelState.IsValid)
                {
                    var author = _mapper.Map<Author>(authorViewModel);

                    var loggedInUserId = _userManager.GetUserId(User);

                    author.CreatedByUserId = author.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                    await _authorService.AddAuthorAsync(author);

                    TempData["SuccessMessage"] = "Author added successfully.";

                    return RedirectToAction(nameof(Index));
                }
                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", authorViewModel.CreatedByUserId);
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", authorViewModel.ModifiedByUserId);

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
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    return NotFound();
                }

                var authorViewModel = _mapper.Map<AuthorViewModel>(author);

                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "City", author.CreatedByUserId);
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "City", author.ModifiedByUserId);
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
                if (_authorService.AuthorExistsInOtherAuthors(authorViewModel.Id, authorViewModel.Name))
                {
                    ModelState.AddModelError("Name", "An author with this Name already exists.");
                }

                if (ModelState.IsValid)
                {
                    var author = _mapper.Map<Author>(authorViewModel);

                    var loggedInUserId = _userManager.GetUserId(User);

                    author.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                    await _authorService.UpdateAuthorAsync(author);

                    TempData["SuccessMessage"] = "Author updated successfully.";

                    return RedirectToAction(nameof(Index));
                }
                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", authorViewModel.CreatedByUserId);
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", authorViewModel.ModifiedByUserId);
                return View(authorViewModel);
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var author = await _authorService.GetAuthorAsync(id);
                if (author != null)
                {
                    var isDeletable = _authorService.IsDeletable(author);
                    if (isDeletable)
                    {
                        await _authorService.RemoveAuthorAsync(author);
                        TempData["SuccessMessage"] = "Author deleted successfully.";
                        return Json(new { success = true, message = "Author deleted successfully." });
                    }
                    
                    TempData["ErrorMessage"] = "Author cannot be deleted because they have associated books.";
                    return Json(new { success = false, message = "Author cannot be deleted because they have associated books." });
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
}
