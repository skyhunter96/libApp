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
using System.Text.RegularExpressions;
using X.PagedList;

namespace LibApp.WebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        private const int PageSize = 10;
        private const string SortTitleOrder = "title_desc";

        public BooksController(LibraryContext context, IBookService bookService, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _bookService = bookService;
            _mapper = mapper;
            _userManager = userManager;
        }

        //Sort by released, qty, created, modified
        //Filter by isAvailable
        //Bulk Delete? after pagination?
        //Delete on Details & Edit?
        //TODO: Needs to check whether files work on laptop
        //TODO: Reservation timer job

        // GET: Books
        public async Task<IActionResult> Index(string sortTitleOrder, string currentTitleFilter, string searchTitleString, 
            int? authorId, int? publisherId, int? categoryId, int? departmentId, int? languageId, int? page)
        {
            ViewBag.CurrentSortTitle = sortTitleOrder;
            ViewBag.SortTitleParm = String.IsNullOrEmpty(sortTitleOrder) ? SortTitleOrder : "";

            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");

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
                var books = _bookService.GetBooks();
                var bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(books);

                if (!string.IsNullOrEmpty(searchTitleString))
                {
                    bookViewModels = bookViewModels.Where(b => b.Title.ToLower().Contains(searchTitleString.ToLower()));
                }

                if (authorId != null)
                {
                    var bookIdsWithAuthor = _bookService.GetBookIdsByAuthorId(authorId.Value);
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
                var book = await _bookService.GetBookAsync(id);

                if (book == null)
                {
                    return NotFound();
                }

                var bookViewModel = _mapper.Map<BookViewModel>(book);

                return View(bookViewModel);
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        // GET: Books/Create
        [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
        public IActionResult Create()
        {
            try
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
                ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
                ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
                ViewData["AuthorIds"] = new MultiSelectList(_context.Authors, "Id", "Name");

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
                if (_bookService.IsbnExists(bookViewModel.Isbn))
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

                    var book = _mapper.Map<Book>(bookViewModel);

                    var loggedInUserId = _userManager.GetUserId(User);

                    book.CreatedByUserId = book.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                    await _bookService.AddBookAsync(book, bookViewModel.AuthorIds, bookViewModel.NewAuthor);

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

                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", bookViewModel.CategoryId);
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", bookViewModel.DepartmentId);
                ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", bookViewModel.LanguageId);
                ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", bookViewModel.PublisherId);
                ViewData["AuthorIds"] = new MultiSelectList(_context.Authors, "Id", "Name", bookViewModel.AuthorIds);

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
                if (id == null || _context.Books == null)
                {
                    return NotFound();
                }

                var book = await _bookService.GetBookAsync(id);

                if (book == null)
                {
                    return NotFound();
                }

                var bookViewModel = _mapper.Map<BookViewModel>(book);

                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", bookViewModel.CategoryId);
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", bookViewModel.DepartmentId);
                ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", bookViewModel.LanguageId);
                ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", bookViewModel.PublisherId);
                ViewData["AuthorIds"] = new MultiSelectList(_context.Authors, "Id", "Name", bookViewModel.AuthorIds);

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
                if (_bookService.IsbnExistsInOtherBooks(bookViewModel.Id, bookViewModel.Isbn))
                {
                    ModelState.AddModelError("Isbn", "A book with this ISBN already exists.");
                }

                if (ModelState.IsValid)
                {
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

                    var book = _mapper.Map<Book>(bookViewModel);

                    var loggedInUserId = _userManager.GetUserId(User);

                    book.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                    await _bookService.UpdateBookAsync(book, bookViewModel.AuthorIds, bookViewModel.NewAuthor);

                    TempData["SuccessMessage"] = "Book updated successfully.";

                    return RedirectToAction(nameof(Index));
                }

                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", bookViewModel.CategoryId);
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", bookViewModel.DepartmentId);
                ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", bookViewModel.LanguageId);
                ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", bookViewModel.PublisherId);
                ViewData["AuthorIds"] = new MultiSelectList(_context.Authors, "Id", "Name", bookViewModel.AuthorIds);

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
        //Validate anti-forgery is left on purpose
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (_context.Books == null)
                {
                    return RedirectToAction("ServerError", "Error");
                }

                var book = await _bookService.GetBookAsync(id);
                if (book != null)
                {
                    await _bookService.RemoveBookAsync(book);
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
}
