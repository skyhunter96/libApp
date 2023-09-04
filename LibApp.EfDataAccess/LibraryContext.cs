using Domain.Models;
using EfDataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EfDataAccess;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public DbSet<BookReservation> BookReservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibApp;Integrated Security=True")
            .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new LanguageConfiguration());
        modelBuilder.ApplyConfiguration(new PublisherConfiguration());
        modelBuilder.ApplyConfiguration(new RateConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new LogConfiguration());
        modelBuilder.ApplyConfiguration(new BookReservationConfiguration());

        // Seed your data here
        SeedData(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var admin = new User
        {
            Id = 1,
            FirstName = "Mladen",
            LastName = "Karic",
            Username = "giomlly",
            Password = "giomlly",
            Email = "misteryx96@yahoo.com",
            RegistrationDateTime = DateTime.Now,
            IsVerified = true,
            Active = true,
            RoleId = (int)Role.Admin,
            DateOfBirth = new DateTime(1996, 2, 7),
            City = "Belgrade",
            Address = "nema ulice bb",
            Phone = "0611234567",
            CardCode = "123-456-789",
            IsCardActive = true,
        };
        var librarian = new User
        {
            Id = 2,
            FirstName = "Mirko",
            LastName = "Cvetkovic",
            Username = "mirko",
            Password = "mirko",
            Email = "mirko@yahoo.com",
            RegistrationDateTime = DateTime.Now,
            IsVerified = true,
            Active = true,
            RoleId = (int)Role.Librarian,
            DateOfBirth = new DateTime(1996, 2, 7),
            City = "Belgrade",
            Address = "nema ulice bb",
            Phone = "0621234567",
            CardCode = "111-456-789",
            IsCardActive = true,
        };
        var regular = new User
        {
            Id = 3,
            FirstName = "Marko",
            LastName = "Nikolic",
            Username = "marko",
            Password = "marko",
            Email = "marko@yahoo.com",
            RegistrationDateTime = DateTime.Now,
            IsVerified = true,
            Active = true,
            RoleId = (int)Role.Regular,
            DateOfBirth = new DateTime(1996, 2, 7),
            City = "Belgrade",
            Address = "nema ulice bb",
            Phone = "0631234567",
            CardCode = "222-456-789",
            IsCardActive = true,
        };

        var users = new List<User> { admin, librarian, regular };

        foreach (var user in users)
        {
            user.CreatedByUserId = admin.Id;
            user.ModifiedByUserId = admin.Id;
        }

        var author1 = new Author
        {
            Id = 1,
            Name = "Ivo Andrić",
        };
        var author2 = new Author
        {
            Id = 2,
            Name = "Fridrih Niče",
        };

        var authors = new List<Author> { author1, author2 };

        foreach (var author in authors)
        {
            author.CreatedByUserId = admin.Id;
            author.ModifiedByUserId = admin.Id;
        }

        var publisher = new Publisher
        {
            Id = 1,
            Name = "Delfi",
            Description = "Delfi knjižare",
            CreatedByUserId = admin.Id,
            ModifiedByUserId = admin.Id
        };
        var category = new Category
        {
            Id = 1,
            Name = "Klasici",
            Description = "Klasici svetske književnosti",
            CreatedByUserId = admin.Id,
            ModifiedByUserId = admin.Id
        };
        var department = new Department
        {
            Id = 1,
            Name = "Klasici",
            Description = "Departman klasika svetske književnosti",
            Budget = 100000,
            CreatedByUserId = admin.Id,
            ModifiedByUserId = admin.Id
        };
        var language1 = new Language
        {
            Id = 1, Name = "Serbian",
        };
        var language2 = new Language
        {
            Id = 2, Name = "English",
        };
        var languages = new List<Language> { language1, language2 };
        var book1 = new Book
        {
            Id = 1,
            Title = "Na Drini Ćuprija",
            Description = "Najpoznatije delo Ive Andrića",
            Isbn = "978-86-6249-252-4",
            Edition = "Second",
            ReleaseYear = 2021,
            PublisherId = publisher.Id,
            CategoryId = category.Id,
            DepartmentId = department.Id,
            LanguageId = language1.Id,
            CreatedByUserId = admin.Id,
            ModifiedByUserId = admin.Id,
            IsAvailable = true,
            Quantity = 10,
            AvailableQuantity = 10,
            ReservedQuantity = 0
        };

        var book2 = new Book
        {
            Id = 2,
            Title = "1984",
            Description = "A dystopian novel by George Orwell",
            Isbn = "978-0-452-28423-4",
            Edition = "First",
            ReleaseYear = 1949,
            PublisherId = publisher.Id,
            CategoryId = category.Id,
            DepartmentId = department.Id,
            LanguageId = language2.Id,
            CreatedByUserId = admin.Id,
            ModifiedByUserId = admin.Id,
            IsAvailable = true,
            Quantity = 15,
            AvailableQuantity = 15,
            ReservedQuantity = 0
        };
        var books = new List<Book> { book1, book2 };

        var bookAuthors = new List<object>
        {
            new { BookId = book1.Id, AuthorId = author1.Id },
            new { BookId = book2.Id, AuthorId = author1.Id },
            new { BookId = book2.Id, AuthorId = author2.Id }
        };

        var rate = new Rate
        {
            Id = 1,
            RateFee = 50,
            ApplyAfterDays = 21,
            CreatedByUserId = admin.Id,
            ModifiedByUserId = admin.Id
        };

        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Author>().HasData(authors);
        modelBuilder.Entity<Publisher>().HasData(publisher);
        modelBuilder.Entity<Category>().HasData(category);
        modelBuilder.Entity<Department>().HasData(department);
        modelBuilder.Entity<Language>().HasData(languages);
        modelBuilder.Entity<Book>().HasData(books);
        modelBuilder.Entity<Book>().HasMany(b => b.Authors).WithMany(a => a.Books)
            .UsingEntity(j => j.HasData(bookAuthors));
        modelBuilder.Entity<Rate>().HasData(rate);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
