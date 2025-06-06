using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Web.Models;

/// <summary>
///     Модель представления для формы входа пользователя.
/// </summary>
public class LoginViewModel
{
    /// <summary>
    ///     Электронная почта пользователя.
    /// </summary>
    [EmailAddress(ErrorMessage = "Адрес почты был указан неверно.")]
    [Required(ErrorMessage = "Требуется почта.")]
    public string? Email { get; init; }

    /// <summary>
    ///     Пароль пользователя.
    /// </summary>
    [Required(ErrorMessage = "Требуется пароль.")]
    [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
    public string? Password { get; init; }
}