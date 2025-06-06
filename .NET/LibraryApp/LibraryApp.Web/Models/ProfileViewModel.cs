namespace LibraryApp.Web.Models;

/// <summary>
///     Модель представления для профиля пользователя.
/// </summary>
public class ProfileViewModel
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     Фамилия пользователя.
    /// </summary>
    public string? Surname { get; init; }

    /// <summary>
    ///     Электронная почта пользователя.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    ///     Список книг пользователя.
    /// </summary>
    public List<BookViewModel>? UserBooks { get; init; }

    /// <summary>
    ///     Список доступных книг.
    /// </summary>
    public List<BookViewModel>? AvailableBooks { get; init; }

    public bool IsAdmin { get; set; }
}