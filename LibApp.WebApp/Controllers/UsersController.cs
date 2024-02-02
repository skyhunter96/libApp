using AutoMapper;
using Domain.Models;
using EfDataAccess;
using LibApp.Services;
using LibApp.Services.Interfaces;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibApp.WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersController(LibraryContext context, IUserService userService, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
        }

        //TODO: Details date of birth only DATE
        //TODO: Activate card action and then the user is active
        //TODO: Change pass action (maybe in manageNavPages
        //TODO: Can't login if not active

        // GET: Users
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);

                return View(userViewModels);
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
                //TODO: Process image - prerequisite create

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
                //TODO: Insert image
                //TODO: CreatedByUserId and UpdatedByUserId need to get from session

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
                //TODO: Insert image
                //TODO: CreatedByUserId and UpdatedByUserId need to get from session

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

                    //var user = _mapper.Map<User>(userViewModel);
                    _mapper.Map(userViewModel, user);

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
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (_context.Users == null)
                {
                    return RedirectToAction("ServerError", "Error");
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
    }
}
