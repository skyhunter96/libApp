namespace LibApp.Services.Interfaces
{
    public interface IReservationService
    {
        bool UserCanReserve(int loggedInUserId);
        bool BookCanBeReserved(int bookId);
        void ReserveBook(int bookId, int loggedInUserId);
    }
}
