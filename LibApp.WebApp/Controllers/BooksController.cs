using AutoMapper;
using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        // GET: Books
        public async Task<IActionResult> Index()
        {
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

                //TODO: Authors shouldn't be in format (1, Ivo Andrić)(2, Fridrih Niče) 
                //TODO: Check when not exist

                var bookViewModel = new BookViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Isbn = book.Isbn,
                    Authors = book.Authors?
                        .Select(author => (AuthorId: author.Id, AuthorName: author.Name)),
                    Edition = book.Edition,
                    ReleaseYear = book.ReleaseYear,
                    Cost = book?.Cost,
                    IsAvailable = book.IsAvailable,
                    Quantity = book.Quantity,
                    AvailableQuantity = book.AvailableQuantity,
                    ReservedQuantity = book.ReservedQuantity,
                    Publisher = book.Publisher?.Name,
                    Category = book.Category?.Name,
                    Department = book.Department?.Name,
                    Language = book.Language?.Name,
                    CreatedDateTime = book.CreatedDateTime,
                    ModifiedDateTime = book.ModifiedDateTime,
                    CreatedByUser = book.CreatedByUser?.Username,
                    ModifiedByUser = book.ModifiedByUser?.Username,
                };

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
            //TODO: Insert image

            try
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
                ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
                ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
                ViewData["Authors"] = new MultiSelectList(_context.Authors, "Id", "Name");
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
        public async Task<IActionResult> Create(
            [Bind(
                "Title,Description,Isbn,Edition,PublisherId,CategoryId,DepartmentId,LanguageId,ImagePath,Cost,IsAvailable,Quantity,AvailableQuantity,ReservedQuantity,Id,CreatedDateTime,ModifiedDateTime,CreatedByUserId,ModifiedByUserId")]
            Book book)
        //TODO: No BookDomain but rather bookViewModel
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
                //TODO: Validate?
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Any())
                    {
                        foreach (var error in entry.Value.Errors)
                        {
                            var errorMessagesListList = new List<string>();
                            errorMessagesListList.Add(error.ErrorMessage);
                            return RedirectToAction("ServerError", "Error");
                        }
                    }
                }

                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", book.DepartmentId);
                ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", book.LanguageId);
                ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", book.PublisherId);
                return View(book);
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", book.DepartmentId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", book.PublisherId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Isbn,Edition,ReleaseYear,PublisherId,CategoryId,DepartmentId,LanguageId,ImagePath,Cost,IsAvailable,Quantity,AvailableQuantity,ReservedQuantity,Id,CreatedDateTime,ModifiedDateTime,CreatedByUserId,ModifiedByUserId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", book.DepartmentId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Department)
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'LibraryContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
