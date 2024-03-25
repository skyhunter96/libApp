using AutoMapper;
using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LibApp.WebApp.Controllers
{
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
    public class UsersController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        private const int PageSize = 10;
        private const string SortNameOrder = "name_desc";

        public UsersController(LibraryContext context, IUserService userService, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index(string sortNameOrder, string currentNameFilter, string searchNameString, 
            int? roleId, int? page)
        {

            ViewBag.CurrentSortName = sortNameOrder;
            ViewBag.SortNameParm = String.IsNullOrEmpty(sortNameOrder) ? SortNameOrder : "";

            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");

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
                var users = await _userService.GetUsersAsync();
                var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);

                if (!string.IsNullOrEmpty(searchNameString))
                {
                    userViewModels = userViewModels.Where(u => u.FullName.ToLower().Contains(searchNameString.ToLower()) || 
                                                               u.UserName.ToLower().Contains(searchNameString.ToLower()) ||
                                                               u.Email.ToLower().Contains(searchNameString.ToLower()));
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
                var user = await _userService.GetUserAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                var userViewModel = _mapper.Map<UserViewModel>(user);

                return View(userViewModel);
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name");
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name");
                ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");

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
                if (_userService.DocumentIdExists(userViewModel.DocumentId))
                {
                    ModelState.AddModelError("DocumentId", "An user with this DocumentId already exists.");
                }

                if (_userService.EmailExists(userViewModel.Email))
                {
                    ModelState.AddModelError("Email", "An user with this Email already exists.");
                }

                if (_userService.UserNameExists(userViewModel.UserName))
                {
                    ModelState.AddModelError("UserName", "An user with this UserName already exists.");
                }

                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<User>(userViewModel);

                    var loggedInUserId = _userManager.GetUserId(User);

                    user.CreatedByUserId = user.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                    await _userService.AddUserAsync(user);

                    TempData["SuccessMessage"] = "User added successfully.";

                    return RedirectToAction(nameof(Index));
                }
                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", userViewModel.CreatedByUserId);
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", userViewModel.ModifiedByUserId);
                ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userViewModel.RoleId);

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
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _userService.GetUserAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                var userViewModel = _mapper.Map<UserViewModel>(user);

                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", user.CreatedByUserId);
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", user.ModifiedByUserId);
                ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);

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

                if (_userService.DocumentIdExistsInOtherBooks(userViewModel.Id, userViewModel.DocumentId))
                {
                    ModelState.AddModelError("DocumentId", "An user with this DocumentId already exists.");
                }

                if (_userService.EmailExistsInOtherBooks(userViewModel.Id, userViewModel.Email))
                {
                    ModelState.AddModelError("Email", "An user with this Email already exists.");
                }

                if (_userService.UserNameExistsInOtherBooks(userViewModel.Id, userViewModel.UserName))
                {
                    ModelState.AddModelError("UserName", "An user with this UserName already exists.");
                }

                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(userViewModel.Id.ToString());

                    if (user == null)
                    {
                        return NotFound();
                    }

                    _mapper.Map(userViewModel, user);

                    var loggedInUserId = _userManager.GetUserId(User);

                    user.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                    await _userService.UpdateUserAsync(user);

                    TempData["SuccessMessage"] = "User updated successfully.";

                    return RedirectToAction(nameof(Index));
                }

                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", userViewModel.CreatedByUserId);
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", userViewModel.ModifiedByUserId);
                ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userViewModel.RoleId);

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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (_context.Users == null)
                {
                    return RedirectToAction("ServerError", "Error");
                }

                var loggedInUserId = Convert.ToInt32(_userManager.GetUserId(User));

                if (loggedInUserId == id)
                {
                    TempData["ErrorMessage"] = "Cannot delete an User you are currently logged in with.";
                    return Json(new { success = false, message = "User currently logged in." });
                }

                var user = await _userService.GetUserAsync(id);

                if (user != null)
                {
                    await _userService.RemoveUserAsync(user);
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
        public IActionResult Activate(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                _userService.Activate(id);

                TempData["SuccessMessage"] = "User activated successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        [Authorize(Roles = AppRoles.Admin)]
        public IActionResult Deactivate(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                _userService.Deactivate(id);

                TempData["SuccessMessage"] = "User deactivated successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }
    }
}
