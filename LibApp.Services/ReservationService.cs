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
                .Include(a => a.ReservedByUser)
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .AsNoTracking()
                .ToListAsync();

            return reservations;
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
    }
}
