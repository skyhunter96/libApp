using AutoMapper;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using X.PagedList;

namespace LibApp.WebApp.Controllers;

public class BooksController(
    IBookService bookService,
    IAuthorService authorService,
    IPublisherService publisherService,
    ICategoryService categoryService,
    IDepartmentService departmentService,
    ILanguageService languageService,
    IMapper mapper,
    UserManager<User> userManager)
    : Controller
{
    private const int PageSize = 10;
    private const string SortTitleOrder = "title_desc";

    //TODO: Sort by released, qty, created, modified
    //TODO: Filter by isAvailable
    //TODO: Services interfaces in another project
    //TODO: Instructions to install on another machine
    //TODO: Reservation timer job

    // GET: Books
    public async Task<IActionResult> Index(string sortTitleOrder, string currentTitleFilter, string searchTitleString, 
        int? authorId, int? publisherId, int? categoryId, int? departmentId, int? languageId, int? page)
    {
        ViewBag.CurrentSortTitle = sortTitleOrder;
        ViewBag.SortTitleParm = String.IsNullOrEmpty(sortTitleOrder) ? SortTitleOrder : "";

        var authorsAsEnumerable = await authorService.GetAuthorsAsync();
        var authors = authorsAsEnumerable.ToList();
        var publishersAsEnumerable = await publisherService.GetPublishersAsync();
        var publishers = publishersAsEnumerable.ToList();
        var categoriesAsEnumerable = await categoryService.GetCategoriesAsync();
        var categories = categoriesAsEnumerable.ToList();
        var departmentsAsEnumerable = await departmentService.GetDepartmentsAsync();
        var departments = departmentsAsEnumerable.ToList();
        var languagesAsEnumerable = await languageService.GetLanguagesAsync();
        var languages = languagesAsEnumerable.ToList();

        ViewData["AuthorId"] = new SelectList(authors, "Id", "Name");
        ViewData["PublisherId"] = new SelectList(publishers, "Id", "Name");
        ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
        ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name");
        ViewData["LanguageId"] = new SelectList(languages, "Id", "Name");

        if (searchTitleString != null)
        {
            page = 1;
        }
        else
        {
            searchTitleString = currentTitleFilter;
        }

        ViewBag.CurrentTitleFilter = searchTitleString;

        try
        {
            var books = await bookService.GetBooksAsync();
            var bookViewModels = mapper.Map<IEnumerable<BookViewModel>>(books);

            if (!string.IsNullOrEmpty(searchTitleString))
            {
                bookViewModels = bookViewModels.Where(b => b.Title.ToLower().Contains(searchTitleString.ToLower()));
            }

            if (authorId != null)
            {
                var bookIdsWithAuthor = await bookService.GetBookIdsByAuthorIdAsync(authorId.Value);
                bookViewModels = bookViewModels.Where(b => bookIdsWithAuthor.Contains(b.Id));
                ViewBag.CurrentAuthorId = authorId;
            }

            if (publisherId != null)
            {
                bookViewModels = bookViewModels.Where(b => b.PublisherId == publisherId);
                ViewBag.CurrentPublisherId = publisherId;
            }

            if (categoryId != null)
            {
                bookViewModels = bookViewModels.Where(b => b.CategoryId == categoryId);
                ViewBag.CurrentCategoryId = categoryId;
            }

            if (departmentId != null)
            {
                bookViewModels = bookViewModels.Where(b => b.DepartmentId == departmentId);
                ViewBag.CurrentDepartmentId = departmentId;
            }

            if (languageId != null)
            {
                bookViewModels = bookViewModels.Where(b => b.LanguageId == languageId);
                ViewBag.CurrentLanguageId = languageId;
            }

            bookViewModels = sortTitleOrder switch
            {
                SortTitleOrder => bookViewModels.OrderByDescending(b => b.Title),
                _ => bookViewModels.OrderBy(b => b.Title)
            };
                
            var pageNumber = (page ?? 1);

            ViewBag.Books = bookViewModels.ToPagedList(pageNumber, PageSize);

            return View();
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Books/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var book = await bookService.GetBookAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var bookViewModel = mapper.Map<BookViewModel>(book);

            return View(bookViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Books/Create
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
    public async Task<IActionResult> Create()
    {
        try
        {
            var authorsAsEnumerable = await authorService.GetAuthorsAsync();
            var authors = authorsAsEnumerable.ToList();
            var publishersAsEnumerable = await publisherService.GetPublishersAsync();
            var publishers = publishersAsEnumerable.ToList();
            var categoriesAsEnumerable = await categoryService.GetCategoriesAsync();
            var categories = categoriesAsEnumerable.ToList();
            var departmentsAsEnumerable = await departmentService.GetDepartmentsAsync();
            var departments = departmentsAsEnumerable.ToList();
            var languagesAsEnumerable = await languageService.GetLanguagesAsync();
            var languages = languagesAsEnumerable.ToList();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(languages, "Id", "Name");
            ViewData["PublisherId"] = new SelectList(publishers, "Id", "Name");
            ViewData["AuthorIds"] = new MultiSelectList(authors, "Id", "Name");

            return View();
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Books/Create
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookViewModel bookViewModel)
    {
        try
        {
            if (await bookService.IsbnExistsAsync(bookViewModel.Isbn))
            {
                ModelState.AddModelError("Isbn", "A book with this ISBN already exists.");
            }

            if (ModelState.IsValid)
            {
                if (bookViewModel.ImageFile is { Length: > 0 })
                {
                    var fileName = bookViewModel.Title.Replace(" ", "_").ToLower();
                    fileName = Regex.Replace(fileName, @"[^\u0000-\u007F]+", string.Empty);
                    fileName = fileName + "_" + DateTime.Now.Ticks + Path.GetExtension(bookViewModel.ImageFile.FileName);

                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "books");

                    // Check if the directory exists, if not, create it
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, fileName);

                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await bookViewModel.ImageFile.CopyToAsync(stream);
                    }

                    // Save the file path to the user object or database
                    bookViewModel.ImagePath = "/img/books/" + fileName;
                }

                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));

                var book = mapper.Map<Book>(
                    bookViewModel,
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = loggedInUserId;
                    });

                await bookService.AddBookAsync(book, bookViewModel.AuthorIds, bookViewModel.NewAuthor);

                TempData["SuccessMessage"] = "Book added successfully.";

                return RedirectToAction(nameof(Index));
            }

            var errorMessageList = new List<string>();

            foreach (var entry in ModelState)
            {
                if (entry.Value.Errors.Any())
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        errorMessageList.Add(error.ErrorMessage);
                        Console.WriteLine($"Property: {entry.Key}, Error: {error.ErrorMessage}");
                    }
                }
            }

            var authorsAsEnumerable = await authorService.GetAuthorsAsync();
            var authors = authorsAsEnumerable.ToList();
            var publishersAsEnumerable = await publisherService.GetPublishersAsync();
            var publishers = publishersAsEnumerable.ToList();
            var categoriesAsEnumerable = await categoryService.GetCategoriesAsync();
            var categories = categoriesAsEnumerable.ToList();
            var departmentsAsEnumerable = await departmentService.GetDepartmentsAsync();
            var departments = departmentsAsEnumerable.ToList();
            var languagesAsEnumerable = await languageService.GetLanguagesAsync();
            var languages = languagesAsEnumerable.ToList();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", bookViewModel.CategoryId);
            ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name", bookViewModel.DepartmentId);
            ViewData["LanguageId"] = new SelectList(languages, "Id", "Name", bookViewModel.LanguageId);
            ViewData["PublisherId"] = new SelectList(publishers, "Id", "Name", bookViewModel.PublisherId);
            ViewData["AuthorIds"] = new MultiSelectList(authors, "Id", "Name", bookViewModel.AuthorIds);

            return View(bookViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Books/Edit/5
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var book = await bookService.GetBookAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var bookViewModel = mapper.Map<BookViewModel>(book);

            var authorsAsEnumerable = await authorService.GetAuthorsAsync();
            var authors = authorsAsEnumerable.ToList();
            var publishersAsEnumerable = await publisherService.GetPublishersAsync();
            var publishers = publishersAsEnumerable.ToList();
            var categoriesAsEnumerable = await categoryService.GetCategoriesAsync();
            var categories = categoriesAsEnumerable.ToList();
            var departmentsAsEnumerable = await departmentService.GetDepartmentsAsync();
            var departments = departmentsAsEnumerable.ToList();
            var languagesAsEnumerable = await languageService.GetLanguagesAsync();
            var languages = languagesAsEnumerable.ToList();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", bookViewModel.CategoryId);
            ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name", bookViewModel.DepartmentId);
            ViewData["LanguageId"] = new SelectList(languages, "Id", "Name", bookViewModel.LanguageId);
            ViewData["PublisherId"] = new SelectList(publishers, "Id", "Name", bookViewModel.PublisherId);
            ViewData["AuthorIds"] = new MultiSelectList(authors, "Id", "Name", bookViewModel.AuthorIds);

            return View(bookViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Books/Edit/5
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BookViewModel bookViewModel)
    {
        if (id != bookViewModel.Id)
        {
            return NotFound();
        }

        try
        {
            if (await bookService.IsbnExistsInOtherBooksAsync(bookViewModel.Id, bookViewModel.Isbn))
            {
                ModelState.AddModelError("Isbn", "A book with this ISBN already exists.");
            }

            if (ModelState.IsValid)
            {
                //TODO: Refactor to methods
                if (bookViewModel.ImageFile is { Length: > 0 })
                {
                    // Delete the old image file
                    if (!string.IsNullOrEmpty(bookViewModel.ImagePath))
                    {
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", bookViewModel.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    var fileName = bookViewModel.Title.Replace(" ", "_").ToLower();
                    fileName = Regex.Replace(fileName, @"[^\u0000-\u007F]+", string.Empty);
                    fileName = fileName + "_" + DateTime.Now.Ticks + Path.GetExtension(bookViewModel.ImageFile.FileName);

                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "books");

                    // Check if the directory exists, if not, create it
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, fileName);

                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await bookViewModel.ImageFile.CopyToAsync(stream);
                    }

                    // Save the file path to the user object or database
                    bookViewModel.ImagePath = "/img/books/" + fileName;
                }

                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));
                var createdByUserId = bookViewModel.CreatedByUserId;

                var book = mapper.Map<Book>(
                    bookViewModel,
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = createdByUserId;
                    });

                await bookService.UpdateBookAsync(book, bookViewModel.AuthorIds, bookViewModel.NewAuthor);

                TempData["SuccessMessage"] = "Book updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            var authorsAsEnumerable = await authorService.GetAuthorsAsync();
            var authors = authorsAsEnumerable.ToList();
            var publishersAsEnumerable = await publisherService.GetPublishersAsync();
            var publishers = publishersAsEnumerable.ToList();
            var categoriesAsEnumerable = await categoryService.GetCategoriesAsync();
            var categories = categoriesAsEnumerable.ToList();
            var departmentsAsEnumerable = await departmentService.GetDepartmentsAsync();
            var departments = departmentsAsEnumerable.ToList();
            var languagesAsEnumerable = await languageService.GetLanguagesAsync();
            var languages = languagesAsEnumerable.ToList();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", bookViewModel.CategoryId);
            ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name", bookViewModel.DepartmentId);
            ViewData["LanguageId"] = new SelectList(languages, "Id", "Name", bookViewModel.LanguageId);
            ViewData["PublisherId"] = new SelectList(publishers, "Id", "Name", bookViewModel.PublisherId);
            ViewData["AuthorIds"] = new MultiSelectList(authors, "Id", "Name", bookViewModel.AuthorIds);

            return View(bookViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Books/Delete/5
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var book = await bookService.GetBookAsync(id);
            if (book != null)
            {
                await bookService.RemoveBookAsync(book);
                TempData["SuccessMessage"] = "Book deleted successfully.";
                return Json(new { success = true, message = "Book deleted successfully." });
            }

            TempData["ErrorMessage"] = "Book was not deleted. An error occurred while processing your request.";
            return Json(new { success = false, message = "Book not found." });
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }
}