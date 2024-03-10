using Domain.Models;

namespace LibApp.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Reservation> GetReservationAsync(int id);
        Task RemoveReservationAsync(Reservation reservation);
        bool UserCanReserve(int loggedInUserId);
        bool BookCanBeReserved(int bookId);
        void ReserveBook(int bookId, int loggedInUserId);
        void StartReservation(int id, int loggedInUserId);
        void FinishReservation(int id, int loggedInUserId);
    }
}
