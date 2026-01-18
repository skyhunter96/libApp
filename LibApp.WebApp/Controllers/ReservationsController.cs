using AutoMapper;
using LibApp.Domain.Models;
using LibApp.EfDataAccess;
using LibApp.Services.Abstractions.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LibApp.WebApp.Controllers;

[Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian + "," + AppRoles.Regular)]
public class ReservationsController : Controller
{
    private readonly LibraryContext _context;
    private readonly IReservationService _reservationService;
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    private const int PageSize = 10;
    private const string SortNameOrder = "name_desc";

    public ReservationsController(LibraryContext context, IReservationService reservationService, UserManager<User> userManager, IMapper mapper, IUserService userService)
    {
        _context = context;
        _reservationService = reservationService;
        _userManager = userManager;
        _mapper = mapper;
        _userService = userService;
    }

    // GET: Reservations
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
            var reservations = await _reservationService.GetReservationsAsync();

            var reservationViewModels = _mapper.Map<IEnumerable<ReservationViewModel>>(reservations);

            if (!string.IsNullOrEmpty(searchNameString))
            {
                reservationViewModels = reservationViewModels.Where(a => a.ReservedByUser != null && a.ReservedByUser.ToLower().Contains(searchNameString.ToLower()));
            }

            reservationViewModels = sortNameOrder switch
            {
                SortNameOrder => reservationViewModels.OrderByDescending(a => a.ReservedByUser),
                _ => reservationViewModels.OrderBy(a => a.ReservedByUser)
            };

            var pageNumber = (page ?? 1);

            ViewBag.Reservations = reservationViewModels.ToPagedList(pageNumber, PageSize);

            return View();
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // GET: Reservations/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            if (id == 0)
            {
                var loggedInUserId = _userManager.GetUserId(User);

                var currentReservationId = await _reservationService.GetReservationIdForUserAsync(Convert.ToInt32(loggedInUserId));

                if (currentReservationId != 0)
                    id = currentReservationId;
            }

            var reservation = await _reservationService.GetReservationAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var reservationViewModel = _mapper.Map<ReservationViewModel>(reservation);

            return View(reservationViewModel);
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    [HttpPost, ActionName("Reserve")]
    public IActionResult Reserve(int bookId)
    {
        try
        {
            var loggedInUserId = _userManager.GetUserId(User);

            if (!_reservationService.UserCanReserve(Convert.ToInt32(loggedInUserId)))
            {
                TempData["ErrorMessage"] = "Book was not reserved. The user has max reservations.";
                return Json(new { success = false, message = "Book was not reserved" });
            }

            if (!_reservationService.BookCanBeReserved(bookId))
            {
                TempData["ErrorMessage"] = "Book was not reserved. It is not available.";
                return Json(new { success = false, message = "Book was not reserved" });
            }

            _reservationService.ReserveBook(bookId, Convert.ToInt32(loggedInUserId));

            TempData["SuccessMessage"] = "Book reserved successfully.";
            return Json(new { success = true, message = "Book reserved successfully." });
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return RedirectToAction("ServerError", "Error");
        }
    }

    public IActionResult Start(int id)
    {
        try
        {
            var loggedInUserId = _userManager.GetUserId(User);

            _reservationService.StartReservation(id, Convert.ToInt32(loggedInUserId));

            TempData["SuccessMessage"] = "Reservation started successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    public IActionResult Finish(int id)
    {
        try
        {
            var loggedInUserId = _userManager.GetUserId(User);

            _reservationService.FinishReservation(id, Convert.ToInt32(loggedInUserId));

            TempData["SuccessMessage"] = "Reservation finished successfully.";

            var loggedInUser = _userService.GetUser(Convert.ToInt32(loggedInUserId));

            return loggedInUser?.RoleId is (int)RoleEnum.Regular ? RedirectToAction("Index", "Home") : RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }

    // POST: Reservations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationAsync(id);
            if (reservation != null)
            {
                await _reservationService.RemoveReservationAsync(reservation);
                TempData["SuccessMessage"] = "Reservation deleted successfully.";
                return Json(new { success = true, message = "Reservation deleted successfully." });
            }

            TempData["ErrorMessage"] = "Reservation was not deleted. An error occurred while processing your request.";
            return Json(new { success = false, message = "Reservation not found." });
        }
        catch (Exception exception)
        {
            return RedirectToAction("ServerError", "Error");
        }
    }
}