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
public class DepartmentsController(IDepartmentService departmentService, IUserService userService, IMapper mapper, UserManager<User> userManager) : Controller
{
    // GET: Departments
    public async Task<IActionResult> Index()
    {
        try
        {
            var departments = await departmentService.GetDepartmentsAsync();
            var departmentViewModels = mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

            return View(departmentViewModels);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Departments/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var department = await departmentService.GetDepartmentAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            var departmentViewModel = mapper.Map<DepartmentViewModel>(department);

            return View(departmentViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Departments/Create
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

    // POST: Departments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DepartmentViewModel departmentViewModel)
    {
        try
        {
            if (await departmentService.DepartmentExistsAsync(departmentViewModel.Name))
            {
                ModelState.AddModelError("Name", "A department with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));

                var department = mapper.Map<Department>(
                    departmentViewModel, 
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = loggedInUserId;
                    });

                await departmentService.AddDepartmentAsync(department);

                TempData["SuccessMessage"] = "Department added successfully.";

                return RedirectToAction(nameof(Index));
            }

            var users = await userService.GetUsersAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", departmentViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", departmentViewModel.ModifiedByUserId);

            return View(departmentViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Departments/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        try
        {
            var department = await departmentService.GetDepartmentAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            var departmentViewModel = mapper.Map<DepartmentViewModel>(department);

            var users = await userService.GetUsersAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", department.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", department.ModifiedByUserId);
            return View(departmentViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Departments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DepartmentViewModel departmentViewModel)
    {
        if (id != departmentViewModel.Id)
        {
            return NotFound();
        }

        try
        {
            if (await departmentService.DepartmentExistsInOtherDepartmentsAsync(departmentViewModel.Id, departmentViewModel.Name))
            {
                ModelState.AddModelError("Name", "An department with this Name already exists.");
            }

            if (ModelState.IsValid)
            {
                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));
                var createdByUserId = departmentViewModel.CreatedByUserId;

                var department = mapper.Map<Department>(
                    departmentViewModel, 
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = createdByUserId;
                    });

                await departmentService.UpdateDepartmentAsync(department);

                TempData["SuccessMessage"] = "Department updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            var users = await userService.GetUsersAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", departmentViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", departmentViewModel.ModifiedByUserId);
            return View(departmentViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Departments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var department = await departmentService.GetDepartmentAsync(id);
            if (department != null)
            {
                var isDeletable = await departmentService.IsDeletableAsync(id);
                if (isDeletable)
                {
                    await departmentService.RemoveDepartmentAsync(department);
                    TempData["SuccessMessage"] = "Department deleted successfully.";
                    return Json(new { success = true, message = "Department deleted successfully." });
                }

                TempData["ErrorMessage"] = "Department cannot be deleted because it has associated books.";
                return Json(new { success = false, message = "Department cannot be deleted because it has associated books." });
            }

            TempData["ErrorMessage"] = "Department was not deleted. An error occurred while processing your request.";
            return Json(new { success = false, message = "Department not found." });
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }
}