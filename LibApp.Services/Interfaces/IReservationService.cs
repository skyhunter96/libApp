using Domain.Models;

namespace LibApp.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        bool UserCanReserve(int loggedInUserId);
        bool BookCanBeReserved(int bookId);
        void ReserveBook(int bookId, int loggedInUserId);
    }
}
