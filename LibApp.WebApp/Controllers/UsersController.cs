using AutoMapper;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LibApp.WebApp.Controllers;

[Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
public class UsersController(IUserService userService, IMapper mapper, UserManager<User> userManager) : Controller
{
    private const int PageSize = 10;
    private const string SortNameOrder = "name_desc";

    // GET: Users
    public async Task<IActionResult> Index(string sortNameOrder, string currentNameFilter, string searchNameString, 
        int? roleId, int? page)
    {

        ViewBag.CurrentSortName = sortNameOrder;
        ViewBag.SortNameParm = String.IsNullOrEmpty(sortNameOrder) ? SortNameOrder : "";

        var roles = await userService.GetRolesAsync();

        ViewData["RoleId"] = new SelectList(roles, "Id", "Name");

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
            var users = await userService.GetUsersAsync();
            var userViewModels = mapper.Map<IEnumerable<UserViewModel>>(users);

            if (!string.IsNullOrEmpty(searchNameString))
            {
                userViewModels = userViewModels.Where(userViewModel => userViewModel.FullName.ToLower().Contains(searchNameString.ToLower()) || 
                                                           userViewModel.UserName.ToLower().Contains(searchNameString.ToLower()) ||
                                                           userViewModel.Email.ToLower().Contains(searchNameString.ToLower()));
            }

            if (roleId != null)
            {
                userViewModels = userViewModels.Where(u => u.RoleId == roleId);
                ViewBag.CurrentRoleId = roleId;
            }

            userViewModels = sortNameOrder switch
            {
                SortNameOrder => userViewModels.OrderByDescending(b => b.FullName),
                _ => userViewModels.OrderBy(b => b.FullName)
            };

            var pageNumber = (page ?? 1);

            ViewBag.Users = userViewModels.ToPagedList(pageNumber, PageSize);

            return View();
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Users/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var user = await userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = mapper.Map<UserViewModel>(user);

            return View(userViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Users/Create
    public async Task<IActionResult> Create()
    {
        try
        {
            var users = await userService.GetUsersAsync();
            var roles = await userService.GetRolesAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name");
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name");
            ViewData["RoleId"] = new SelectList(roles, "Id", "Name");

            return View();
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Users/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserViewModel userViewModel)
    {
        try
        {
            if (await userService.DocumentIdExistsAsync(userViewModel.DocumentId))
            {
                ModelState.AddModelError("DocumentId", "An user with this DocumentId already exists.");
            }

            if (await userService.EmailExistsAsync(userViewModel.Email))
            {
                ModelState.AddModelError("Email", "An user with this Email already exists.");
            }

            if (await userService.UserNameExistsAsync(userViewModel.UserName))
            {
                ModelState.AddModelError("UserName", "An user with this UserName already exists.");
            }

            if (ModelState.IsValid)
            {
                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));

                var user = mapper.Map<User>(
                    userViewModel,
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = loggedInUserId;
                    });

                await userService.AddUserAsync(user);

                TempData["SuccessMessage"] = "User added successfully.";

                return RedirectToAction(nameof(Index));
            }

            var users = await userService.GetUsersAsync();
            var roles = await userService.GetRolesAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", userViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", userViewModel.ModifiedByUserId);
            ViewData["RoleId"] = new SelectList(roles, "Id", "Name", userViewModel.RoleId);

            return View(userViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Users/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            if (id == 0)
            {
                return NotFound();
            }

            var user = await userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = mapper.Map<UserViewModel>(user);

            var users = await userService.GetUsersAsync();
            var roles = await userService.GetRolesAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", user.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", user.ModifiedByUserId);
            ViewData["RoleId"] = new SelectList(roles, "Id", "Name", user.RoleId);

            return View(userViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Users/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UserViewModel userViewModel)
    {
        if (id != userViewModel.Id)
        {
            return NotFound();
        }

        try
        {
            ViewData.ModelState.Remove("Password");

            if (await userService.DocumentIdExistsInOtherBooksAsync(userViewModel.Id, userViewModel.DocumentId))
            {
                ModelState.AddModelError("DocumentId", "An user with this DocumentId already exists.");
            }

            if (await userService.EmailExistsInOtherBooksAsync(userViewModel.Id, userViewModel.Email))
            {
                ModelState.AddModelError("Email", "An user with this Email already exists.");
            }

            if (await userService.UserNameExistsInOtherBooksAsync(userViewModel.Id, userViewModel.UserName))
            {
                ModelState.AddModelError("UserName", "An user with this UserName already exists.");
            }

            if (ModelState.IsValid)
            {
                var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));
                var createdByUserId = userViewModel.CreatedByUserId;

                var user = await userManager.FindByIdAsync(userViewModel.Id.ToString());

                if (user == null)
                {
                    return NotFound();
                }

                //TODO: This needs to be checked

                mapper.Map(
                    userViewModel, 
                    user,
                    options =>
                    {
                        options.Items["LoggedInUserId"] = loggedInUserId;
                        options.Items["CreatedByUserId"] = createdByUserId;
                    });

                await userService.UpdateUserAsync(user);

                TempData["SuccessMessage"] = "User updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            var users = await userService.GetUsersAsync();
            var roles = await userService.GetRolesAsync();

            ViewData["CreatedByUserId"] = new SelectList(users, "Id", "Name", userViewModel.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(users, "Id", "Name", userViewModel.ModifiedByUserId);
            ViewData["RoleId"] = new SelectList(roles, "Id", "Name", userViewModel.RoleId);

            return View(userViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Users/Delete/5
    [Authorize(Roles = AppRoles.Admin)]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var users = await userService.GetUsersAsync();

            if (users == null)
            {
                return RedirectToAction("ServerError", "Error");
            }

            var loggedInUserId = Convert.ToInt32(userManager.GetUserId(User));

            if (loggedInUserId == id)
            {
                TempData["ErrorMessage"] = "Cannot delete an User you are currently logged in with.";
                return Json(new { success = false, message = "User currently logged in." });
            }

            var user = await userService.GetUserAsync(id);

            if (user != null)
            {
                await userService.RemoveUserAsync(user);
                TempData["SuccessMessage"] = "User deleted successfully.";
                return Json(new { success = true, message = "User deleted successfully." });
            }

            TempData["ErrorMessage"] = "User was not deleted. An error occurred while processing your request.";
            return Json(new { success = false, message = "User not found." });
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    [Authorize(Roles = AppRoles.Admin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ActivateAsync(int id)
    {
        try
        {
            if (id == 0)
            {
                return NotFound();
            }

            await userService.ActivateAsync(id);

            TempData["SuccessMessage"] = "User activated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    [Authorize(Roles = AppRoles.Admin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeactivateAsync(int id)
    {
        try
        {
            if (id == 0)
            {
                return NotFound();
            }

            await userService.DeactivateAsync(id);

            TempData["SuccessMessage"] = "User deactivated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }
}