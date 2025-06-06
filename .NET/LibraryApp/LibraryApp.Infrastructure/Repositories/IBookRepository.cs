using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Repositories;

namespace LibraryApp.Infrastructure.Repositories;

/// <summary>
///     Интерфейс для репозитория, работающего с сущностями книг в базе данных.
/// </summary>
public interface IBookRepository : IRepository<BookEntity>
{
    /// <summary>
    ///     Получить все книги, принадлежащие пользователю с указанным идентификатором.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чьи книги необходимо получить.</param>
    /// <returns>
    ///     Возвращает коллекцию книг, принадлежащих пользователю с указанным идентификатором.<br />
    ///     Если книги не найдены, возвращается пустой список.
    /// </returns>
    Task<ICollection<BookEntity>> GetByUserId(int userId);

    Task<BookEntity?> GetByTitleAndAuthor(string title, string author);

    Task<ICollection<BookEntity>> GetAllFree();
}