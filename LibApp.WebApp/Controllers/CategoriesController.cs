using AutoMapper;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibApp.WebApp.Controllers;

[Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
public class CategoriesController(UserManager<User> userManager, IMapper mapper, ICategoryService categoryService, IUserService userService) : Controller
{
    // GET: Categories
    public async Task<IActionResult> Index()
    {
        try
        {
            var categories = await categoryService.GetCategoriesAsync();
            var categoryViewModels = mapper.Map<IEnumerable<CategoryViewModel>>(categories);

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
            var category = await categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = mapper.Map<CategoryViewModel>(category);

            return View(categoryViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Categories/Create
    public async Task<IActionResult> Create()
    {
        try
        {
            var users = await userService.GetUsersAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name");
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name");

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
            if (await categoryService.CategoryExistsAsync(categoryViewModel.Name))
            {
                ModelState.AddModelError("Name", "A category with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));
                var createdByUserId = categoryViewModel.CreatedByUserId;

                var category = mapper.Map<Category>(
                    categoryViewModel,
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = createdByUserId;
                    });

                await categoryService.AddCategoryAsync(category);

                TempData["SuccessMessage"] = "Category added successfully.";

                return RedirectToAction(nameof(Index));
            }

            var users = await userService.GetUsersAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", categoryViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", categoryViewModel.ModifiedByUserId);

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
        if (id == 0)
        {
            return NotFound();
        }

        try
        {
            var category = await categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = mapper.Map<CategoryViewModel>(category);

            var users = await userService.GetUsersAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", category.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", category.ModifiedByUserId);
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
            if (await categoryService.CategoryExistsInOtherCategoriesAsync(categoryViewModel.Id, categoryViewModel.Name))
            {
                ModelState.AddModelError("Name", "An Category with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));
                var createdByUserId = categoryViewModel.CreatedByUserId;

                var category = mapper.Map<Category>(
                    categoryViewModel,
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = createdByUserId;
                    });

                await categoryService.UpdateCategoryAsync(category);

                TempData["SuccessMessage"] = "Category updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            var users = await userService.GetUsersAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", categoryViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", categoryViewModel.ModifiedByUserId);
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
            var category = await categoryService.GetCategoryAsync(id);
            if (category != null)
            {
                var isDeletable = await categoryService.IsDeletableAsync(id);
                if (isDeletable)
                {
                    await categoryService.RemoveCategoryAsync(category);
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