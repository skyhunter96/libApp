using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.EfDataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace LibApp.EfDataAccess.Repositories;

public class UserRepository(LibraryContext context) : GenericRepository<User>(context), IUserRepository
{
    private bool _modifiedBookQuantities;

    public async Task<IEnumerable<User>> GetAllWithRolesAsync()
    {
        return await Query()
            .Include(u => u.CreatedByUser)
            .Include(u => u.ModifiedByUser)
            .Include(u => u.Role)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetByIdWithRolesAsync(int id)
    {
        return await Query()
            .Include(u => u.CreatedByUser)
            .Include(u => u.ModifiedByUser)
            .Include(u => u.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task SeverRelationsAsync(User user)
    {
        foreach (var book in context.Books.Where(b => b.CreatedByUserId == user.Id))
        {
            book.SetCreatedByUserId(null);
        }

        foreach (var book in context.Books.Where(b => b.ModifiedByUserId == user.Id))
        {
            book.SetModifiedByUserId(null);
        }

        foreach (var category in context.Categories.Where(b => b.CreatedByUserId == user.Id))
        {
            category.SetCreatedByUserId(null);
        }

        foreach (var category in context.Categories.Where(b => b.ModifiedByUserId == user.Id))
        {
            category.SetModifiedByUserId(null);
        }

        foreach (var author in context.Authors.Where(b => b.CreatedByUserId == user.Id))
        {
            author.SetCreatedByUserId(null);
        }

        foreach (var author in context.Authors.Where(b => b.ModifiedByUserId == user.Id))
        {
            author.SetModifiedByUserId(null);
        }

        foreach (var publisher in context.Publishers.Where(b => b.CreatedByUserId == user.Id))
        {
            publisher.SetCreatedByUserId(null);
        }

        foreach (var publisher in context.Publishers.Where(b => b.ModifiedByUserId == user.Id))
        {
            publisher.SetModifiedByUserId(null);
        }

        foreach (var department in context.Departments.Where(b => b.CreatedByUserId == user.Id))
        {
            department.SetCreatedByUserId(null);
        }

        foreach (var department in context.Departments.Where(b => b.ModifiedByUserId == user.Id))
        {
            department.SetModifiedByUserId(null);
        }

        foreach (var reservation in context.Reservations.Where(b => b.CreatedByUserId == user.Id)
                     .Include(reservation => reservation.BookReservations)
                     .ThenInclude(bookReservation => bookReservation.Book))
        {
            if (!_modifiedBookQuantities)
            {
                foreach (var bookReservation in reservation.BookReservations)
                {
                    bookReservation.Book.ReservedQuantity--;
                    bookReservation.Book.AvailableQuantity++;

                    _modifiedBookQuantities = true;
                }
            }
            
            context.BookReservations.RemoveRange(reservation.BookReservations);
            context.Reservations.Remove(reservation);
        }

        foreach (var reservation in context.Reservations.Where(b => b.ModifiedByUserId == user.Id)
                     .Include(reservation => reservation.BookReservations)
                     .ThenInclude(bookReservation => bookReservation.Book))
        {
            if (!_modifiedBookQuantities)
            {
                foreach (var bookReservation in reservation.BookReservations)
                {
                    bookReservation.Book.ReservedQuantity--;
                    bookReservation.Book.AvailableQuantity++;

                    _modifiedBookQuantities = true;
                }
            }

            context.BookReservations.RemoveRange(reservation.BookReservations);
            context.Reservations.Remove(reservation);
        }

        foreach (var userToChange in context.Users.Where(b => b.CreatedByUserId == user.Id))
        {
            userToChange.SetCreatedByUserId(null);
        }

        foreach (var userToChange in context.Users.Where(b => b.ModifiedByUserId == user.Id))
        {
            userToChange.SetModifiedByUserId(null);
        }

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await context.Roles.AsNoTracking().ToListAsync();
    }

    public async Task<Role?> GetRoleForUserIdAsync(int id)
    {
        var user = await GetByIdWithRolesAsync(id);
        return user?.Role;
    }
}