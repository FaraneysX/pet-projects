namespace LibraryApp.Web.Models;

/// <summary>
///     Модель представления для книги.
/// </summary>
public class BookViewModel
{
    /// <summary>
    ///     Название книги.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    ///     Автор книги.
    /// </summary>
    public string? Author { get; init; }
}