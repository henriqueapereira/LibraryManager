using LibraryManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Persistence;

public class LibraryManagerDbContext : DbContext
{
    public LibraryManagerDbContext(DbContextOptions<LibraryManagerDbContext> options) : base(options)
    {

    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);

            e.HasMany(u => u.Loans) // 1 usuario tem muitos empréstimos
                .WithOne(us => us.User)// 1 empréstimo tem 1 unico usuario
                .HasForeignKey(us => us.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Loan>(e =>
        {
            e.HasKey(l => l.Id);

            e.HasOne(l => l.User) //1 empréstimo tem 1 usuario
                .WithMany(e => e.Loans) //1 usuario pode ter muitos empréstimos
                .HasForeignKey(e => e.IdUser);

            e.HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.IdBook);
        });

        builder.Entity<Book>(e =>
        {
            e.HasKey(b => b.Id);

            e.HasMany(b => b.Loans)
                .WithOne(l => l.Book)
                .HasForeignKey(l => l.IdBook)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(builder);
    }
}
