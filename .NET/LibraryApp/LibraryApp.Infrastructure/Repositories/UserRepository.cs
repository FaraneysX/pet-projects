using LibraryApp.Domain.Entities;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories;

/// <summary>
///     Репозиторий для работы с сущностями пользователей в базе данных.
/// </summary>
/// <param name="dbContext">Контекст базы данных.</param>
public class UserRepository(ApplicationDbContext dbContext) : BaseRepository<UserEntity>(dbContext), IUserRepository
{
    /// <inheritdoc />
    public async Task<UserEntity?> GetByEmail(string email)
    {
        return await DbContext.UserEntities
            .Include(userEntity => userEntity.Role)
            .FirstOrDefaultAsync(e => e.Email == email);
    }

    /// <inheritdoc />
    public bool VerifyByPassword(UserEntity user, string password)
    {
        return user.Password == password;
    }

    public async Task AddBookToUser(int userId, BookEntity book)
    {
        var user = await DbContext.UserEntities.Include(userEntity => userEntity.Books)
            .FirstOrDefaultAsync(e => e.Id == userId);

        user?.Books?.Add(book);
        book.User = user;

        await SaveChanges();
    }

    public async Task RemoveBookFromUser(int userId, BookEntity book)
    {
        var user = await DbContext.UserEntities.Include(userEntity => userEntity.Books)
            .FirstOrDefaultAsync(e => e.Id == userId);

        user?.Books?.Remove(book);
        book.User = null;

        await SaveChanges();
    }
}