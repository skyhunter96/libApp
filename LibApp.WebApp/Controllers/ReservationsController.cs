using AutoMapper;
using Domain.Models;
using EfDataAccess;
using LibApp.Services;
using LibApp.Services.Interfaces;
using LibApp.WebApp.Utilities;
using LibApp.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LibApp.WebApp.Controllers
{
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian + "," + AppRoles.Regular)]
    public class ReservationsController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IReservationService _reservationService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        private const int PageSize = 10;
        private const string SortNameOrder = "name_desc";

        public ReservationsController(LibraryContext context, IReservationService reservationService, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _reservationService = reservationService;
            _userManager = userManager;
            _mapper = mapper;
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
                    reservationViewModels = reservationViewModels.Where(a => a.ReservedByUser.ToLower().Contains(searchNameString.ToLower()));
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
                TempData["SuccessMessage"] = "Reservation finished successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                return RedirectToAction("ServerError", "Error");
            }
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
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
}
