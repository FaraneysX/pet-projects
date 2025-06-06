using LibraryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data;

/// <summary>
///     Контекст базы данных.
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    /// <summary>
    ///     Представление таблицы пользователей в базе данных.
    /// </summary>
    public DbSet<UserEntity> UserEntities { get; init; }

    /// <summary>
    ///     Представление таблицы книг в базе данных.
    /// </summary>
    public DbSet<BookEntity> BookEntities { get; init; }

    /// <summary>
    ///     Представление таблицы ролей пользователей в базе данных.
    /// </summary>
    public DbSet<UserEntityRole> UserEntityRoles { get; init; }

    /// <summary>
    ///     Конфигурировать модель данных для сущностей.
    /// </summary>
    /// <param name="modelBuilder">Объект, который позволяет конфигурировать модель данных.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookEntity>()
            .HasOne(book => book.User)
            .WithMany(user => user.Books)
            .HasForeignKey("user_id");

        modelBuilder.Entity<UserEntity>()
            .HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .HasForeignKey("role_id");

        modelBuilder.Entity<UserEntityRole>()
            .HasIndex(role => role.Name)
            .IsUnique();

        modelBuilder.Entity<UserEntityRole>()
            .HasData(
                new UserEntityRole { Id = 1, Name = "User" },
                new UserEntityRole { Id = 2, Name = "Admin" }
            );
    }
}