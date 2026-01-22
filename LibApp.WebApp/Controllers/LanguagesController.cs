using AutoMapper;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibApp.WebApp.Controllers;

[Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
public class LanguagesController(ILanguageService languageService,UserManager<User> userManager, IMapper mapper) : Controller
{
    private readonly UserManager<User> _userManager = userManager;

    // GET: Languages
    public async Task<IActionResult> Index()
    {
        try
        {
            var languages = await languageService.GetLanguagesAsync();
            var languageViewModels = mapper.Map<IEnumerable<LanguageViewModel>>(languages);

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
            var language = await languageService.GetLanguageAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            var languageViewModel = mapper.Map<LanguageViewModel>(language);

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
            if (await languageService.LanguageExistsAsync(languageViewModel.Name))
            {
                ModelState.AddModelError("Name", "A language with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var language = mapper.Map<Language>(languageViewModel);

                await languageService.AddLanguageAsync(language);

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
        if (id == 0)
        {
            return NotFound();
        }

        try
        {
            var language = await languageService.GetLanguageAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            var languageViewModel = mapper.Map<LanguageViewModel>(language);

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
            if (await languageService.LanguageExistsInOtherLanguagesAsync(languageViewModel.Id, languageViewModel.Name))
            {
                ModelState.AddModelError("Name", "An language with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var language = mapper.Map<Language>(languageViewModel);

                await languageService.UpdateLanguageAsync(language);

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
            var language = await languageService.GetLanguageAsync(id);
            if (language != null)
            {
                var isDeletable = await languageService.IsDeletableAsync(id);
                if (isDeletable)
                {
                    await languageService.RemoveLanguageAsync(language);
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