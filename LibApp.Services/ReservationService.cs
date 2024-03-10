using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibApp.Services
{
    public class ReservationService : IReservationService
    {
        private readonly LibraryContext _context;

        private const int ReservationNumber = 3;

        public ReservationService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.BookReservations)
                .Include(a => a.ReservedByUser)
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .AsNoTracking()
                .ToListAsync();

            return reservations;
        }

        public async Task<Reservation> GetReservationAsync(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.BookReservations)
                .ThenInclude(br => br.Book)
                .Include(a => a.ReservedByUser)
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .FirstOrDefaultAsync(r => r.Id == id);

            return reservation;
        }

        public async Task<int> GetReservationIdForUserAsync(int userId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.BookReservations)
                .ThenInclude(br => br.Book)
                .Include(a => a.ReservedByUser)
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReservedByUserId == userId);

            return reservation?.Id ?? 0;
        }

        public async Task RemoveReservationAsync(Reservation reservation)
        {
            foreach (var bookReservation in reservation.BookReservations)
            {
                bookReservation.Book.ReservedQuantity--;
                bookReservation.Book.AvailableQuantity++;
            }

            _context.BookReservations.RemoveRange(reservation.BookReservations);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        //Reservation can have up to three bookReservations (books)
        public bool UserCanReserve(int loggedInUserId)
        {
            var reservation = _context.Reservations
                .Include(reservation => reservation.BookReservations)
                .FirstOrDefault(r => r.CreatedByUserId == loggedInUserId);

            if (reservation == null)
            {
                return true;
            }

            return reservation.BookReservations.Count < ReservationNumber;
        }

        public bool BookCanBeReserved(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (book == null || book.IsAvailable == false || book.AvailableQuantity == 0)
            {
                return false;
            }

            return true;
        }

        public void ReserveBook(int bookId, int loggedInUserId)
        {
            //First create single Reservation entity

            var reservation = _context.Reservations
                .Include(r => r.BookReservations)
                .Include(r => r.ReservedByUser)
                .Include(r=> r.CreatedByUser)
                .Include(r=> r.ModifiedByUser)
                .FirstOrDefault(r => r.CreatedByUserId == loggedInUserId);

            if (reservation == null)
            {
                reservation = new Reservation();
                reservation.ReservedByUserId = (int)(reservation.CreatedByUserId = loggedInUserId);

                reservation.DueDate = DateTime.Now.AddDays(21);

                _context.Reservations.Add(reservation);
            }
            reservation.ModifiedDateTime = DateTime.Now;
            reservation.ModifiedByUserId = loggedInUserId;
            reservation.IsStarted = false;

            //Then add single bookReservation object

            var bookReservation = new BookReservation();
            bookReservation.CreatedDateTime = bookReservation.ModifiedDateTime = DateTime.Now;

            bookReservation.BookId = bookId;

            reservation.BookReservations ??= new List<BookReservation>();
            reservation.BookReservations.Add(bookReservation);

            //Then adjust book quantities

            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                book.ReservedQuantity++;
                book.AvailableQuantity--;
            }

            _context.SaveChanges();
        }

        public void StartReservation(int id, int loggedInUserId)
        {
            var reservation = _context.Reservations
                .Include(r => r.BookReservations)
                .ThenInclude(br => br.Book)
                .Include(a => a.ReservedByUser)
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .FirstOrDefault(r => r.Id == id);

            reservation.LoanDate = DateTime.Now;
            reservation.DueDate = DateTime.Now.AddDays(21);
            reservation.ModifiedByUserId = loggedInUserId;
            reservation.IsStarted = true;

            _context.SaveChanges();
        }

        public void FinishReservation(int id, int loggedInUserId)
        {
            var reservation = _context.Reservations
                .Include(r => r.BookReservations)
                .ThenInclude(br => br.Book)
                .Include(a => a.ReservedByUser)
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .FirstOrDefault(r => r.Id == id);

            if (reservation != null)
            {
                foreach (var bookReservation in reservation.BookReservations)
                {
                    bookReservation.Book.ReservedQuantity--;
                    bookReservation.Book.AvailableQuantity++;
                }

                if (reservation.BookReservations != null)
                    _context.BookReservations.RemoveRange(reservation.BookReservations);

                _context.Reservations.Remove(reservation);

                _context.SaveChanges();
            }
        }
    }
}
