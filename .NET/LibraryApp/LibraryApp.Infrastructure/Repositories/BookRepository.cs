using LibraryApp.Domain.Entities;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories;

/// <summary>
///     Репозиторий для работы с сущностями книг в базе данных.
/// </summary>
/// <param name="dbContext">Контекст базы данных.</param>
public class BookRepository(ApplicationDbContext dbContext) : BaseRepository<BookEntity>(dbContext), IBookRepository
{
    /// <inheritdoc />
    public async Task<ICollection<BookEntity>> GetByUserId(int userId)
    {
        return await DbContext.BookEntities.Where(book => book.User!.Id == userId).ToListAsync();
    }

    public async Task<BookEntity?> GetByTitleAndAuthor(string title, string author)
    {
        return await DbContext.BookEntities.Where(book => book.Title == title && book.Author == author)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<BookEntity>> GetAllFree()
    {
        return await DbContext.BookEntities.Where(book => book.User == null).ToListAsync();
    }
}