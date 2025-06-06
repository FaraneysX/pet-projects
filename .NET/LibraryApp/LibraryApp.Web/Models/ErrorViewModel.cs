namespace LibraryApp.Web.Models;

/// <summary>
///     Модель представления ошибки.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    ///     Идентификатор запроса.
    /// </summary>
    public string? RequestId { get; init; }

    /// <summary>
    ///     Флаг, который указывает, следует ли показывать идентификатор запроса на странице ошибки.
    ///     Возвращает <c>true</c>, если RequestId не пуст.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    /// <summary>
    ///     Код статуса ошибки.
    /// </summary>
    public int? StatusCode { get; init; }
}