using AutoMapper;
using LibApp.Domain.Models;
using LibApp.EfDataAccess;
using LibApp.Services.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LibApp.WebApp.Controllers;

[Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
public class PublishersController : Controller
{
    private readonly LibraryContext _context;
    private readonly IPublisherService _publisherService;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    private const int PageSize = 10;
    private const string SortNameOrder = "name_desc";

    public PublishersController(LibraryContext context, IPublisherService publisherService, UserManager<User> userManager, IMapper mapper)
    {
        _context = context;
        _publisherService = publisherService;
        _userManager = userManager;
        _mapper = mapper;
    }

    // GET: Publishers
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
            var publishers = await _publisherService.GetPublishersAsync();
            var publisherViewModels = _mapper.Map<IEnumerable<PublisherViewModel>>(publishers);

            if (!string.IsNullOrEmpty(searchNameString))
            {
                publisherViewModels = publisherViewModels.Where(a => a.Name.ToLower().Contains(searchNameString.ToLower()));
            }

            publisherViewModels = sortNameOrder switch
            {
                SortNameOrder => publisherViewModels.OrderByDescending(a => a.Name),
                _ => publisherViewModels.OrderBy(a => a.Name)
            };
            var pageNumber = (page ?? 1);

            ViewBag.Publishers = publisherViewModels.ToPagedList(pageNumber, PageSize);

            return View();
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Publishers/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var publisher = await _publisherService.GetPublisherAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisher);

            return View(publisherViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Publishers/Create
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

    // POST: Publishers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PublisherViewModel publisherViewModel)
    {
        try
        {
            if (_publisherService.PublisherExists(publisherViewModel.Name))
            {
                ModelState.AddModelError("Name", "A Publisher with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var publisher = _mapper.Map<Publisher>(publisherViewModel);

                var loggedInUserId = _userManager.GetUserId(User);

                publisher.CreatedByUserId = publisher.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                await _publisherService.AddPublisherAsync(publisher);

                TempData["SuccessMessage"] = "Publisher added successfully.";

                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", publisherViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", publisherViewModel.ModifiedByUserId);

            return View(publisherViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Publishers/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisher);

            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", publisher.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", publisher.ModifiedByUserId);
            return View(publisherViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Publishers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PublisherViewModel publisherViewModel)
    {
        if (id != publisherViewModel.Id)
        {
            return NotFound();
        }

        try
        {
            if (_publisherService.PublisherExistsInOtherPublishers(publisherViewModel.Id, publisherViewModel.Name))
            {
                ModelState.AddModelError("Name", "An Publisher with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var publisher = _mapper.Map<Publisher>(publisherViewModel);

                var loggedInUserId = _userManager.GetUserId(User);

                publisher.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                await _publisherService.UpdatePublisherAsync(publisher);

                TempData["SuccessMessage"] = "Publisher updated successfully.";

                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", publisherViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", publisherViewModel.ModifiedByUserId);
            return View(publisherViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Publishers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var publisher = await _publisherService.GetPublisherAsync(id);
            if (publisher != null)
            {
                var isDeletable = _publisherService.IsDeletable(publisher);
                if (isDeletable)
                {
                    await _publisherService.RemovePublisherAsync(publisher);
                    TempData["SuccessMessage"] = "Publisher deleted successfully.";
                    return Json(new { success = true, message = "Publisher deleted successfully." });
                }

                TempData["ErrorMessage"] = "Publisher cannot be deleted because it has associated books.";
                return Json(new { success = false, message = "Publisher cannot be deleted because it has associated books." });
            }

            TempData["ErrorMessage"] = "Publisher was not deleted. An error occurred while processing your request.";
            return Json(new { success = false, message = "Publisher not found." });
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }
}