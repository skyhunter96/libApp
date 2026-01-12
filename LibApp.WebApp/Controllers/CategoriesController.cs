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

namespace LibApp.WebApp.Controllers;

[Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
public class CategoriesController : Controller
{
    private readonly LibraryContext _context;
    private readonly ICategoryService _categoryService;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CategoriesController(LibraryContext context, UserManager<User> userManager, IMapper mapper, ICategoryService categoryService)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        try
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var categoryViewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            return View(categoryViewModels);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var category = await _categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Categories/Create
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

    // POST: Categories/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
    {
        try
        {
            if (_categoryService.CategoryExists(categoryViewModel.Name))
            {
                ModelState.AddModelError("Name", "A category with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(categoryViewModel);

                var loggedInUserId = _userManager.GetUserId(User);

                category.CreatedByUserId = category.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                await _categoryService.AddCategoryAsync(category);

                TempData["SuccessMessage"] = "Category added successfully.";

                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", categoryViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", categoryViewModel.ModifiedByUserId);

            return View(categoryViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", category.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", category.ModifiedByUserId);
            return View(categoryViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Categories/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryViewModel categoryViewModel)
    {
        if (id != categoryViewModel.Id)
        {
            return NotFound();
        }

        try
        {
            if (_categoryService.CategoryExistsInOtherCategories(categoryViewModel.Id, categoryViewModel.Name))
            {
                ModelState.AddModelError("Name", "An Category with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(categoryViewModel);

                var loggedInUserId = _userManager.GetUserId(User);

                category.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                await _categoryService.UpdateCategoryAsync(category);

                TempData["SuccessMessage"] = "Category updated successfully.";

                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", categoryViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", categoryViewModel.ModifiedByUserId);
            return View(categoryViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category != null)
            {
                var isDeletable = _categoryService.IsDeletable(category);
                if (isDeletable)
                {
                    await _categoryService.RemoveCategoryAsync(category);
                    TempData["SuccessMessage"] = "Category deleted successfully.";
                    return Json(new { success = true, message = "Category deleted successfully." });
                }

                TempData["ErrorMessage"] = "Category cannot be deleted because it has associated books.";
                return Json(new { success = false, message = "Category cannot be deleted because it has associated books." });
            }

            TempData["ErrorMessage"] = "Category was not deleted. An error occurred while processing your request.";
            return Json(new { success = false, message = "Category not found." });
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }
}