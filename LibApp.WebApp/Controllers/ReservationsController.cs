using AutoMapper;
using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using LibApp.WebApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibApp.WebApp.Controllers
{
    [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Librarian + "," + AppRoles.Regular)]
    public class ReservationsController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IReservationService _reservationService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ReservationsController(LibraryContext context, IReservationService reservationService, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _reservationService = reservationService;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Reservations.Include(r => r.CreatedByUser).Include(r => r.ModifiedByUser).Include(r => r.ReservedByUser);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.CreatedByUser)
                .Include(r => r.ModifiedByUser)
                .Include(r => r.ReservedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        public IActionResult Reserve(int bookId)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            var url = Url.Action("Index", "Books");

            if (!_reservationService.UserCanReserve(Convert.ToInt32(loggedInUserId)))
            {
                TempData["ErrorMessage"] = "Book was not reserved. The user has max reservations.";
                return Json(new { redirectUrl = url, success = false, message = "Book was not reserved" });
            }

            if (!_reservationService.BookCanBeReserved(bookId))
            {
                TempData["ErrorMessage"] = "Book was not reserved. It is not available.";
                return Json(new { redirectUrl = url, success = false, message = "Book was not reserved" });
            }

            _reservationService.ReserveBook(bookId, Convert.ToInt32(loggedInUserId));

            TempData["SuccessMessage"] = "Book reserved successfully.";
            return Json(new { redirectUrl = url, success = true, message = "Book reserved successfully." });
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["ReservedByUserId"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanDate,DueDate,ActualReturnDate,LateFee,ReservedByUserId,Id,CreatedDateTime,ModifiedDateTime,CreatedByUserId,ModifiedByUserId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", reservation.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", reservation.ModifiedByUserId);
            ViewData["ReservedByUserId"] = new SelectList(_context.Users, "Id", "Name", reservation.ReservedByUserId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", reservation.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", reservation.ModifiedByUserId);
            ViewData["ReservedByUserId"] = new SelectList(_context.Users, "Id", "Name", reservation.ReservedByUserId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanDate,DueDate,ActualReturnDate,LateFee,ReservedByUserId,Id,CreatedDateTime,ModifiedDateTime,CreatedByUserId,ModifiedByUserId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", reservation.CreatedByUserId);
            ViewData["ModifiedByUserId"] = new SelectList(_context.Users, "Id", "Name", reservation.ModifiedByUserId);
            ViewData["ReservedByUserId"] = new SelectList(_context.Users, "Id", "Name", reservation.ReservedByUserId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.CreatedByUser)
                .Include(r => r.ModifiedByUser)
                .Include(r => r.ReservedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
