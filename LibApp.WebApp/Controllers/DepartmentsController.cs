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

namespace LibApp.WebApp.Controllers
{
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian)]
    public class DepartmentsController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IDepartmentService _departmentService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public DepartmentsController(LibraryContext context, IDepartmentService departmentService, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _departmentService = departmentService;
            _mapper = mapper;
            _userManager = userManager;
        }

        //TODO: Delete behavior with existing related entities - don't allow, alert - not possible cuz related?

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            try
            {
                var departments = await _departmentService.GetDepartmentsAsync();
                var departmentViewModels = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

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
                var department = await _departmentService.GetDepartmentAsync(id);

                if (department == null)
                {
                    return NotFound();
                }

                var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);

                return View(departmentViewModel);
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        // GET: Departments/Create
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

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentViewModel)
        {
            try
            {
                if (_departmentService.DepartmentExists(departmentViewModel.Name))
                {
                    ModelState.AddModelError("Name", "An department with this Name already exists.");
                }

                if (ModelState.IsValid)
                {
                    var department = _mapper.Map<Department>(departmentViewModel);

                    var loggedInUserId = _userManager.GetUserId(User);

                    department.CreatedByUserId = department.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                    await _departmentService.AddDepartmentAsync(department);

                    TempData["SuccessMessage"] = "Department added successfully.";

                    return RedirectToAction(nameof(Index));
                }
                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", departmentViewModel.CreatedByUserId);
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", departmentViewModel.ModifiedByUserId);

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
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var department = await _context.Departments.FindAsync(id);

                if (department == null)
                {
                    return NotFound();
                }

                var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);

                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", department.CreatedByUserId);
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", department.ModifiedByUserId);
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
                if (_departmentService.DepartmentExistsInOtherDepartments(departmentViewModel.Id, departmentViewModel.Name))
                {
                    ModelState.AddModelError("Name", "An department with this Name already exists.");
                }

                if (ModelState.IsValid)
                {
                    var department = _mapper.Map<Department>(departmentViewModel);

                    var loggedInUserId = _userManager.GetUserId(User);

                    department.ModifiedByUserId = Convert.ToInt32(loggedInUserId);

                    await _departmentService.UpdateDepartmentAsync(department);

                    TempData["SuccessMessage"] = "Department updated successfully.";

                    return RedirectToAction(nameof(Index));
                }
                ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", departmentViewModel.CreatedByUserId);
                ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", departmentViewModel.ModifiedByUserId);
                return View(departmentViewModel);
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentAsync(id);
                if (department != null)
                {
                    var isDeletable = _departmentService.IsDeletable(department);
                    if (isDeletable)
                    {
                        await _departmentService.RemoveDepartmentAsync(department);
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
}
