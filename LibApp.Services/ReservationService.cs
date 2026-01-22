using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;

namespace LibApp.Services;

public class ReservationService(IReservationRepository reservationRepository, IBookRepository bookRepository) : IReservationService
{
    private const int MaxReservationNumber = 3;

    public async Task<IEnumerable<Reservation>> GetReservationsAsync()
    {
        return await reservationRepository.GetAllWithRelatedEntitiesAsync();
    }

    public async Task<Reservation?> GetReservationAsync(int id)
    {
        return await reservationRepository.GetByWithRelatedEntitiesAsync(id);
    }

    public async Task<int> GetReservationIdForUserAsync(int userId)
    {
        var reservation = await reservationRepository.GetReservationForUserAsync(userId);

        return reservation?.Id ?? 0;
    }

    public async Task RemoveReservationAsync(int id)
    {
        var reservation = await GetReservationAsync(id);

        if (reservation != null)
        {
            foreach (var bookReservation in reservation.BookReservations)
            {
                bookReservation.Book.ReservedQuantity--;
                bookReservation.Book.AvailableQuantity++;
            }

            await reservationRepository.RemoveReservationAsync(reservation);
        }
    }

    //Reservation can have up to three bookReservations (books)
    public async Task<bool> UserCanReserveAsync(int loggedInUserId)
    {
        var reservations = await reservationRepository.GetAllWithRelatedEntitiesAsync();
        var reservation = reservations.FirstOrDefault(r => r.CreatedByUserId == loggedInUserId);

        if (reservation == null)
        {
            return true;
        }

        return reservation.BookReservations.Count < MaxReservationNumber;
    }

    public async Task<bool> BookCanBeReservedAsync(int bookId)
    {
        var book = await bookRepository.GetByIdAsync(bookId);

        if (book == null || book.IsAvailable == false || book.AvailableQuantity == 0)
        {
            return false;
        }

        return true;
    }

    public async Task ReserveBookAsync(int bookId, int loggedInUserId)
    {
        //First create single Reservation entity
        var reservationsAsEnumerable = await reservationRepository.GetAllWithRelatedEntitiesAsync();
        var reservations = reservationsAsEnumerable.ToList();
        var reservation = reservations.FirstOrDefault(r => r.CreatedByUserId == loggedInUserId);

        if (reservation == null)
        {
            reservation = new Reservation
            {
                ReservedByUserId = loggedInUserId,
                DueDate = DateTime.Now.AddDays(21)
            };
            reservation.SetCreatedByUserId(loggedInUserId);
            reservations.Add(reservation);
        }
        reservation.SetModifiedDateTime(DateTime.Now);
        reservation.SetModifiedByUserId(loggedInUserId);
        reservation.IsStarted = false;

        //Then add single bookReservation object
        var bookReservation = new BookReservation
        {
            CreatedDateTime = DateTime.Now
        };
        bookReservation.SetModifiedDateTime(DateTime.Now);
        bookReservation.BookId = bookId;
        reservation.AddBookReservations(bookReservation);

        //Then adjust book quantities
        var book = await bookRepository.GetByIdAsync(bookId);

        if (book != null)
        {
            book.ReservedQuantity++;
            book.AvailableQuantity--;
        }

        await reservationRepository.UpdateReservationWithBookAsync(reservation, book);
    }

    public async Task StartReservationAsync(int id, int loggedInUserId)
    {
        var reservation = await GetReservationAsync(id);

        if (reservation == null)
        {
            throw new ArgumentNullException(nameof(reservation));
        }

        reservation.LoanDate = DateTime.Now;
        reservation.DueDate = DateTime.Now.AddDays(21);
        reservation.SetModifiedByUserId(loggedInUserId);
        reservation.IsStarted = true;

        await reservationRepository.UpdateAsync(reservation);
    }
}