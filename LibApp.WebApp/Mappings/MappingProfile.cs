using AutoMapper;
using LibApp.Domain.Models;
using LibApp.WebApp.ViewModels;

namespace LibApp.WebApp.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //TODO: Extract to methods

        CreateMap<Book, BookViewModel>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => MapAuthors(src.Authors)))
            .ForMember(dest => dest.AuthorIds, opt => opt.MapFrom(src => src.Authors.Select(author => author.Id)))
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser!.UserName))
            .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser!.UserName))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.Name))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department!.Name))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.Name));

        CreateMap<BookViewModel, Book>()
            .ForMember(d => d.CreatedByUserId, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUserId, opt => opt.Ignore())
            .ForMember(d => d.CreatedByUser, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUser, opt => opt.Ignore())
            .ConstructUsing((src, context) =>
            {
                var loggedInUserId = context.Items["LoggedInUserId"];
                var createdByUserId = context.Items["CreatedByUserId"];

                var book = new Book
                {
                    Id = src.Id,
                    Title = src.Title,
                    Description = src.Description ?? string.Empty,
                    Isbn = src.Isbn,
                    Edition = src.Edition,
                    ReleaseYear = src.ReleaseYear,
                    PublisherId = src.PublisherId ?? 0,
                    CategoryId = src.CategoryId ?? 0,
                    DepartmentId = src.DepartmentId,
                    LanguageId = src.LanguageId ?? 0,
                    Cost = src.Cost,
                    IsAvailable = src.IsAvailable,
                    Quantity = src.Quantity,
                    AvailableQuantity = src.AvailableQuantity,
                    ReservedQuantity = src.ReservedQuantity,
                    ImagePath = src.ImagePath ?? string.Empty,
                };
                if (createdByUserId != null)
                {
                    book.SetCreatedByUserId((int)createdByUserId);
                }
                book.SetModifiedByUserId((int)loggedInUserId);
                
                if (src.AuthorIds != null)
                {
                    foreach (var authorId in src.AuthorIds)
                    {
                        book.Authors.Add(new Author { Id = authorId });
                    }
                }

                return book;
            })
            .AfterMap((src, dest) =>
            {
                if (src.Authors != null)
                {
                    foreach (var (AuthorId, AuthorName) in src.Authors)
                    {
                        if (dest.Authors.All(a => a.Id != AuthorId))
                        {
                            dest.Authors.Add(new Author { Id = AuthorId, Name = AuthorName });
                        }
                    }
                }
            });

        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
            .ForMember(dest => dest.DateOfBirthToShow, opt => opt.MapFrom(src => src.DateOfBirth.ToString("d.M.yyyy.")));

        CreateMap<UserViewModel, User>()
            .ForMember(d => d.CreatedByUserId, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUserId, opt => opt.Ignore())
            .ForMember(d => d.CreatedByUser, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUser, opt => opt.Ignore())
            .ConstructUsing((src, context) =>
            {
                var loggedInUserId = context.Items["LoggedInUserId"];
                var createdByUserId = context.Items["CreatedByUserId"];

                var user = new User
                {
                    Id = src.Id,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    UserName = src.UserName,
                    Email = src.Email,
                    Password = src.Password,
                    PhoneNumber = src.PhoneNumber,
                    DocumentId = src.DocumentId,
                    IsActive = src.IsActive,
                    ImagePath = src.ImagePath!,
                    DateOfBirth = src.DateOfBirth,
                    City = src.City,
                    Address = src.Address,
                    CardCode = src.CardCode,
                    TotalFee = src.TotalFee,
                    Notes = src.Notes,
                    RoleId = src.RoleId,
                };
                if (createdByUserId != null)
                {
                    user.SetCreatedByUserId((int)createdByUserId);
                }
                user.SetModifiedByUserId((int)loggedInUserId);

                return user;
            });

        CreateMap<Author, AuthorViewModel>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser!.UserName))
            .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser!.UserName));

        CreateMap<AuthorViewModel, Author>()
            .ForMember(d => d.CreatedByUserId, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUserId, opt => opt.Ignore())
            .ForMember(d => d.CreatedByUser, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUser, opt => opt.Ignore())
            .ConstructUsing((src, context) =>
            {
                var loggedInUserId = context.Items["LoggedInUserId"];
                var createdByUserId = context.Items["CreatedByUserId"];

                var author = new Author
                {
                    Id = src.Id,
                    Name = src.Name,
                };
                if (createdByUserId != null)
                {
                    author.SetCreatedByUserId((int)createdByUserId);
                }
                author.SetModifiedByUserId((int)loggedInUserId);
                
                return author;
            });

        CreateMap<Department, DepartmentViewModel>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser!.UserName))
            .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser!.UserName));

        CreateMap<DepartmentViewModel, Department>()
            .ForMember(d => d.CreatedByUserId, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUserId, opt => opt.Ignore())
            .ForMember(d => d.CreatedByUser, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUser, opt => opt.Ignore())
            .ConstructUsing((src, context) =>
            {
                var loggedInUserId = context.Items["LoggedInUserId"];
                var createdByUserId = context.Items["CreatedByUserId"];

                var department = new Department
                {
                    Id = src.Id,
                    Name = src.Name,
                    Description = src.Description,
                    Location = src.Location,
                    Budget = src.Budget,
                };
                if (createdByUserId != null)
                {
                    department.SetCreatedByUserId((int)createdByUserId);
                }
                department.SetModifiedByUserId((int)loggedInUserId);

                return department;
            });

        CreateMap<Category, CategoryViewModel>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser!.UserName))
            .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser!.UserName));

        CreateMap<CategoryViewModel, Category>()
            .ForMember(d => d.CreatedByUserId, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUserId, opt => opt.Ignore())
            .ForMember(d => d.CreatedByUser, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUser, opt => opt.Ignore())
            .ConstructUsing((src, context) =>
            {
                var loggedInUserId = context.Items["LoggedInUserId"];
                var createdByUserId = context.Items["CreatedByUserId"];

                var category = new Category
                {
                    Id = src.Id,
                    Name = src.Name,
                    Description = src.Description,
                };
                if (createdByUserId != null)
                {
                    category.SetCreatedByUserId((int)createdByUserId);
                }
                category.SetModifiedByUserId((int)loggedInUserId);

                return category;
            });

        CreateMap<Publisher, PublisherViewModel>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser!.UserName))
            .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser!.UserName));

        CreateMap<PublisherViewModel, Publisher>()
            .ForMember(d => d.CreatedByUserId, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUserId, opt => opt.Ignore())
            .ForMember(d => d.CreatedByUser, opt => opt.Ignore())
            .ForMember(d => d.ModifiedByUser, opt => opt.Ignore())
            .ConstructUsing((src, context) =>
            {
                var loggedInUserId = context.Items["LoggedInUserId"];
                var createdByUserId = context.Items["CreatedByUserId"];

                var publisher = new Publisher
                {
                    Id = src.Id,
                    Name = src.Name,
                    Description = src.Description,
                };
                if (createdByUserId != null)
                {
                    publisher.SetCreatedByUserId((int)createdByUserId);
                }
                publisher.SetModifiedByUserId((int)loggedInUserId);

                return publisher;
            });

        CreateMap<Language, LanguageViewModel>();

        CreateMap<LanguageViewModel, Language>();

        CreateMap<Reservation, ReservationViewModel>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => MapBooks(src.BookReservations)))
            .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.BookReservations.Select(book => book.BookId)))
            .ForMember(dest => dest.ReservedByUser, opt => opt.MapFrom(src => src.ReservedByUser!.UserName))
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser!.UserName))
            .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => src.ModifiedByUser!.UserName));

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