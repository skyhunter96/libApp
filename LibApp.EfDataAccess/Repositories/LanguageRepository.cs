using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.EfDataAccess.Repositories.Common;

namespace LibApp.EfDataAccess.Repositories;

public class LanguageRepository(LibraryContext context) : GenericRepository<Language>(context), ILanguageRepository
{
}