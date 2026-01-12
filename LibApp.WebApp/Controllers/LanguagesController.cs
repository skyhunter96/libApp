using AutoMapper;
using LibApp.Domain.Models;
using LibApp.EfDataAccess;
using LibApp.Services.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibApp.WebApp.Controllers;

[Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
public class LanguagesController : Controller
{
    private readonly LibraryContext _context;
    private readonly ILanguageService _languageService;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public LanguagesController(LibraryContext context, ILanguageService languageService, UserManager<User> userManager, IMapper mapper)
    {
        _context = context;
        _languageService = languageService;
        _userManager = userManager;
        _mapper = mapper;
    }

    // GET: Languages
    public async Task<IActionResult> Index()
    {
        try
        {
            var languages = await _languageService.GetLanguagesAsync();
            var languageViewModels = _mapper.Map<IEnumerable<LanguageViewModel>>(languages);

            return View(languageViewModels);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Languages/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var language = await _languageService.GetLanguageAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            var languageViewModel = _mapper.Map<LanguageViewModel>(language);

            return View(languageViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Languages/Create
    public IActionResult Create()
    {
        try
        {
            return View();
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Languages/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LanguageViewModel languageViewModel)
    {
        try
        {
            if (_languageService.LanguageExists(languageViewModel.Name))
            {
                ModelState.AddModelError("Name", "A language with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var language = _mapper.Map<Language>(languageViewModel);

                var loggedInUserId = _userManager.GetUserId(User);

                await _languageService.AddLanguageAsync(language);

                TempData["SuccessMessage"] = "Language added successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View(languageViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Languages/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            var languageViewModel = _mapper.Map<LanguageViewModel>(language);

            return View(languageViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Languages/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, LanguageViewModel languageViewModel)
    {
        if (id != languageViewModel.Id)
        {
            return NotFound();
        }

        try
        {
            if (_languageService.LanguageExistsInOtherLanguages(languageViewModel.Id, languageViewModel.Name))
            {
                ModelState.AddModelError("Name", "An language with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var language = _mapper.Map<Language>(languageViewModel);

                var loggedInUserId = _userManager.GetUserId(User);

                await _languageService.UpdateLanguageAsync(language);

                TempData["SuccessMessage"] = "Language updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View(languageViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Languages/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var language = await _languageService.GetLanguageAsync(id);
            if (language != null)
            {
                var isDeletable = _languageService.IsDeletable(language);
                if (isDeletable)
                {
                    await _languageService.RemoveLanguageAsync(language);
                    TempData["SuccessMessage"] = "Language deleted successfully.";
                    return Json(new { success = true, message = "Language deleted successfully." });
                }

                TempData["ErrorMessage"] = "Language cannot be deleted because it has associated books.";
                return Json(new { success = false, message = "Language cannot be deleted because it has associated books." });
            }

            TempData["ErrorMessage"] = "Language was not deleted. An error occurred while processing your request.";
            return Json(new { success = false, message = "Language not found." });
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }
}