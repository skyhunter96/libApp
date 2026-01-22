using LibApp.Data.Abstractions.Interfaces.Common;
using LibApp.Domain.Models;

namespace LibApp.Data.Abstractions.Interfaces;

public interface IReservationRepository : IGenericRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetAllWithRelatedEntitiesAsync();
    Task<Reservation?> GetByWithRelatedEntitiesAsync(int id);
    Task<Reservation?> GetReservationForUserAsync(int userId);
    Task RemoveReservationAsync(Reservation reservation);
    Task UpdateReservationWithBookAsync(Reservation reservation, Book book);
}