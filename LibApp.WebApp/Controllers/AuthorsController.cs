using AutoMapper;
using Domain.Models;
using EfDataAccess;
using LibApp.Services;
using LibApp.Services.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LibApp.WebApp.Controllers
{
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
    public class AuthorsController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        private const int PageSize = 10;
        private const string SortNameOrder = "name_desc";

        public AuthorsController(LibraryContext context, IAuthorService authorService, IMapper mapper)
        {
            _context = context;
            _authorService = authorService;
            _mapper = mapper;
        }

        //TODO: Links to users createdBy/modBy on index, details
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
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "City");
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "City");
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedDateTime,ModifiedDateTime,CreatedByUserId,ModifiedByUserId")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "City", author.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "City", author.ModifiedByUserId);
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "City", author.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "City", author.ModifiedByUserId);
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,CreatedDateTime,ModifiedDateTime,CreatedByUserId,ModifiedByUserId")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "City", author.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "City", author.ModifiedByUserId);
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
