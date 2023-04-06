using Domain.Models;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibApp;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Authors", "lib");

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books", "lib");

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories", "lib");

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Departments", "lib");

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Languages", "lib");

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.ToTable("Publishers", "lib");

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.ToTable("Rates", "lib");

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservations", "lib");

            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.HasKey(e => e.Id);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
