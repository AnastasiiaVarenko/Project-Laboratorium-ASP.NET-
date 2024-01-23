using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<AthorsDetailsEntity> AthorsDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string dbFilePath = Path.Combine(currentDirectory, "database.db");
        SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
        builder.DataSource = dbFilePath;
        string connectionString = builder.ConnectionString;
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite(connectionString);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var user = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "adam",
            NormalizedUserName = "ADAM",
            Email = "adam@wsei.edu.pl",
            NormalizedEmail = "ADAM@WSEI.EDU.PL",
            EmailConfirmed = true,

        };

        PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "1234Abcd$");
        modelBuilder.Entity<IdentityUser>().HasData(user);

        var adminRole = new IdentityRole()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "admin",
            NormalizedName = "ADMIN"
        };
        adminRole.ConcurrencyStamp = adminRole.Id;

        modelBuilder.Entity<IdentityRole>().HasData(adminRole);
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData
            (
                new IdentityUserRole<string>()
                {
                    RoleId = adminRole.Id,
                    UserId = user.Id,
                }
            );

        modelBuilder.Entity<OrganizationEntity>().HasData
        (
            new OrganizationEntity() { Id = 101, Name = "WSEI", Description = "Uczelnia wyzsza" },
            new OrganizationEntity() { Id = 102, Name = "Comarch", Description = "Przedsiębiorstwo IT" }
        );
        modelBuilder.Entity<ContactEntity>().HasData
        (
            new ContactEntity() { ContactId = 1, Name = "Adam", Email = "adam@wsei.edu.pl", Phone = "124124234", Birth = DateTime.Parse("2000-10-10"), Created = DateTime.Parse("2000-10-10"), Priority = Priority.Low, OrganizationId = 101 },
            new ContactEntity() { ContactId = 2, Name = "John", Email = "john@email.com", Phone = "987654321", Birth = DateTime.Parse("1995-05-15"), Created = DateTime.UtcNow, Priority = Priority.Low, OrganizationId = 101 },
            new ContactEntity() { ContactId = 3, Name = "Alice", Email = "alice@email.com", Phone = "123456789", Birth = DateTime.Parse("1990-08-20"), Created = DateTime.UtcNow, Priority = Priority.Normal, OrganizationId = 102 },
            new ContactEntity() { ContactId = 4, Name = "Bob", Email = "bob@email.com", Phone = "555555555", Birth = DateTime.Parse("1985-11-25"), Created = DateTime.UtcNow, Priority = Priority.Urgent, OrganizationId = 102 }
        );
        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(o => o.Adress)
            .HasData
            (
                new { OrganizationEntityId = 101, City = "Krakow", Street = "św. Filipa 17", PostalCode = "31-150" },
                new { OrganizationEntityId = 102, City = "Krakow", Street = "Rozwoju 1/4", PostalCode = "36-160" }
            );

        modelBuilder.Entity<PostEntity>().HasData(
           new PostEntity()
           {
               PostEntityId = 1,
               PublicationDate = DateTime.Parse("2024-01-01"),
               Content = "Routery Wi-Fi 6E: najmocniejsze rozwiązanie na rynku",
               ContactEntityContactId = 1,
               ContactName = "Adam"
           },
           new PostEntity()
           {
               PostEntityId = 2,
               PublicationDate = DateTime.Parse("2012-12-12"),
               Content = "Król Danii odwiedzi Szczecin",
               ContactEntityContactId = 3,
               ContactName = "John"
           }
       );

        modelBuilder.Entity<AthorsDetailsEntity>().HasData(
            new AthorsDetailsEntity()
            {
                Id = 1,
                Name = "Adam",
                Email = "adam@wsei.edu.pl",
                Phone = "124124234",
            },
            new AthorsDetailsEntity()
            {
                Id = 2,
                Name = "John",
                Email = "john@email.com",
                Phone = "987654321",
            }
        );

        modelBuilder.Entity<PostEntity>()
            .OwnsOne(a => a.Adress)
            .HasData(
            new { PostEntityId = 1, City = "Krakow", Street = "Rynek Główny 22", PostalCode = "31-075" },
            new { PostEntityId = 2, City = "Krakow", Street = "Aleja Pokoju 67", PostalCode = "31-382" }
            );

    }
}
