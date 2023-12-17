using AutoMapper;
using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibApp.WebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(LibraryContext context, IBookService bookService, IMapper mapper)
        {
            _context = context;
            _bookService = bookService;
            _mapper = mapper;
        }

        //TODO: Authorize actions
        //TODO: Reservation timer

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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            try
            {
                //TODO: Insert image
                //TODO: CreatedByUserId and UpdatedByUserId need to get from session

                if (_bookService.IsbnExists(model.Isbn))
                {
                    ModelState.AddModelError("Isbn", "A book with this ISBN already exists.");
                }

                if (ModelState.IsValid)
                {
                    var book = _mapper.Map<Book>(model);

                    await _bookService.AddBookAsync(book, model.AuthorIds, model.NewAuthor);

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
                            //TODO: to check if to log somewhere
                            errorMessageList.Add(error.ErrorMessage);
                            Console.WriteLine($"Property: {entry.Key}, Error: {error.ErrorMessage}");
                        }
                    }
                }

                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", model.DepartmentId);
                ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", model.LanguageId);
                ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", model.PublisherId);
                ViewData["AuthorIds"] = new MultiSelectList(_context.Authors, "Id", "Name", model.AuthorIds);

                return View(model);
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        // GET: Books/Edit/5
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookViewModel bookViewModel)
        {
            //TODO: Edit image
            //TODO: CreatedByUserId and UpdatedByUserId need to get from session

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
        [HttpPost, ActionName("Delete")]
        //TODO: validate anti-forgery?
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
                    TempData["SuccessMessage"] = "Book added successfully.";
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
