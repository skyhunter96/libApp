using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetReservationsAsync();
    Task<Reservation?> GetReservationAsync(int id);
    Task<int> GetReservationIdForUserAsync(int userId);
    Task RemoveReservationAsync(int id);
    Task<bool> UserCanReserveAsync(int loggedInUserId);
    Task<bool> BookCanBeReservedAsync(int bookId);
    Task ReserveBookAsync(int bookId, int loggedInUserId);
    Task StartReservationAsync(int id, int loggedInUserId);
}