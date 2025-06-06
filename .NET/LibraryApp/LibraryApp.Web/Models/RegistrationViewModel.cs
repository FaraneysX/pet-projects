using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Web.Models;

/// <summary>
///     Модель представления для профиля пользователя.
/// </summary>
public class RegistrationViewModel
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    [Required(ErrorMessage = "Требуется имя.")]
    public string? Name { get; init; }

    /// <summary>
    ///     Фамилия пользователя.
    /// </summary>
    [Required(ErrorMessage = "Требуется фамилия.")]
    public string? Surname { get; init; }

    /// <summary>
    ///     Электронная почта пользователя.
    /// </summary>
    [EmailAddress]
    [Required(ErrorMessage = "Требуется почта.")]
    public string? Email { get; init; }

    /// <summary>
    ///     Пароль пользователя.
    /// </summary>
    [Required(ErrorMessage = "Требуется пароль.")]
    [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
    public string? Password { get; init; }
}