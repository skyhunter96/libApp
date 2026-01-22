using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.EfDataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace LibApp.EfDataAccess.Repositories;

public class ReservationRepository(LibraryContext context) : GenericRepository<Reservation>(context), IReservationRepository
{
    public async Task<IEnumerable<Reservation>> GetAllWithRelatedEntitiesAsync()
    {
        return await context.Reservations
            .Include(r => r.BookReservations)
            .ThenInclude(br => br.Book)
            .Include(a => a.ReservedByUser)
            .Include(a => a.CreatedByUser)
            .Include(a => a.ModifiedByUser)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Reservation?> GetByWithRelatedEntitiesAsync(int id)
    {
        return await context.Reservations
            .Include(r => r.BookReservations)
            .ThenInclude(br => br.Book)
            .Include(a => a.ReservedByUser)
            .Include(a => a.CreatedByUser)
            .Include(a => a.ModifiedByUser)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Reservation?> GetReservationForUserAsync(int userId)
    { 
        return await context.Reservations
            .Include(r => r.BookReservations)
            .ThenInclude(br => br.Book)
            .Include(a => a.ReservedByUser)
            .Include(a => a.CreatedByUser)
            .Include(a => a.ModifiedByUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReservedByUserId == userId);
    }

    public async Task RemoveReservationAsync(Reservation reservation)
    {
        if (reservation.BookReservations.Any())
            context.BookReservations.RemoveRange(reservation.BookReservations);

        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync();
    }

    public async Task UpdateReservationWithBookAsync(Reservation reservation, Book book)
    {
        context.Reservations.Update(reservation);
        context.Books.Update(book);
        await context.SaveChangesAsync();
    }
}