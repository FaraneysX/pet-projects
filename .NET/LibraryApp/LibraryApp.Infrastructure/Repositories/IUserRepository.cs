using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Repositories;

namespace LibraryApp.Infrastructure.Repositories;

/// <summary>
///     Интерфейс для репозитория, работающего с сущностями пользователей в базе данных.
/// </summary>
public interface IUserRepository : IRepository<UserEntity>
{
    /// <summary>
    ///     Получить пользователя по указанному адресу электронной почты.
    /// </summary>
    /// <param name="email">Адрес электронной почты пользователя.</param>
    /// <returns>Возвращает пользователя с указанным адресом электронной почты, если он найден;<br /> иначе <c>null</c>.</returns>
    Task<UserEntity?> GetByEmail(string email);

    /// <summary>
    ///     Проверить, соответствует ли указанный пароль для пользователя.
    /// </summary>
    /// <param name="user">Пользователь, для которого нужно проверить пароль.</param>
    /// <param name="password">Пароль, который нужно проверить.</param>
    /// <returns>Возвращает <c>true</c>, если пароль совпадает с паролем пользователя;<br /> иначе <c>false</c>.</returns>
    bool VerifyByPassword(UserEntity user, string password);

    Task AddBookToUser(int userId, BookEntity book);

    Task RemoveBookFromUser(int userId, BookEntity book);
}