using AutoMapper;
using Domain.Models;
using LibApp.WebApp.ViewModels;

namespace LibApp.WebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => MapAuthors(src.Authors)))
                .ForMember(dest => dest.AuthorIds, opt => opt.MapFrom(src => src.Authors.Select(author => author.Id)))
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser.UserName))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.Name));

            CreateMap<BookViewModel, Book>();


            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.DateOfBirthToShow, opt => opt.MapFrom(src => src.DateOfBirth.ToString("d.M.yyyy.")));

            CreateMap<UserViewModel, User>();


            CreateMap<Author, AuthorViewModel>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser.UserName));

            CreateMap<AuthorViewModel, Author>();


            CreateMap<Department, DepartmentViewModel>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser.UserName));

            CreateMap<DepartmentViewModel, Department>();


            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser.UserName));

            CreateMap<CategoryViewModel, Category>();


            CreateMap<Publisher, PublisherViewModel>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser.UserName));

            CreateMap<PublisherViewModel, Publisher>();


            CreateMap<Language, LanguageViewModel>();

            CreateMap<LanguageViewModel, Language>();


            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => MapBooks(src.BookReservations)))
                .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.BookReservations.Select(book => book.BookId)))
                .ForMember(dest => dest.ReservedByUser, opt => opt.MapFrom(src => src.ReservedByUser.UserName))
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser.UserName));

            CreateMap<ReservationViewModel, Reservation>();
        }

        private IEnumerable<(int AuthorId, string AuthorName)> MapAuthors(IEnumerable<Author> authors)
        {
            return authors.Select(author => (AuthorId: author.Id, AuthorName: author.Name));
        }

        private IEnumerable<(int? BookId, string? BookName)> MapBooks(IEnumerable<BookReservation> books)
        {
            return books.Select(book => (BookId: book?.BookId, BookName: book?.Book?.Title));
        }
    }
}