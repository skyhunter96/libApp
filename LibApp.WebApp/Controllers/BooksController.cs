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

namespace LibApp.WebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public BooksController(LibraryContext context, IBookService bookService, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _bookService = bookService;
            _mapper = mapper;
            _userManager = userManager;
        }

        //TODO: Paginate with search
        //TODO: Links from details and other pages to resources (authors, departments etc)
        //TODO: Reservation timer job
        //TODO: Bulk Delete?
        //TODO: Delete on Edit?

        // GET: Books
        public async Task<IActionResult> Index()
        {
            //TODO: Pagination

            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            try
            {
                var books = _bookService.GetBooks();
                var bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(books);

                //stopwatch.Stop();
                //var executionTime = stopwatch.ElapsedMilliseconds;
                return View(bookViewModels);
            }
            catch (Exception exception)
            {
                //stopwatch.Stop();
                return RedirectToAction("ServerError", "Error");
            }
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                //TODO: Process image - prerequisite create

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
                //TODO: Insert image

                if (_bookService.IsbnExists(bookViewModel.Isbn))
                {
                    ModelState.AddModelError("Isbn", "A book with this ISBN already exists.");
                }

                if (ModelState.IsValid)
                {
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
            //TODO: Edit image

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
