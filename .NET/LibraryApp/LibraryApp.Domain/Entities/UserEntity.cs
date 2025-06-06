using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Domain.Entities;

/// <summary>
///     Сущность: пользователь.
/// </summary>
[Table("user")]
public record UserEntity : BaseEntity
{
    /// <summary>
    ///     Имя.
    /// </summary>
    [Column("name")]
    public required string Name { get; init; }

    /// <summary>
    ///     Фамилия.
    /// </summary>
    [Column("surname")]
    public required string Surname { get; init; }

    /// <summary>
    ///     Почта.
    /// </summary>
    [Column("email")]
    public required string Email { get; init; }

    /// <summary>
    ///     Пароль.
    /// </summary>
    [Column("password")]
    public required string Password { get; init; }

    /// <summary>
    ///     Роль.
    /// </summary>
    [Column("role")]
    public required UserEntityRole Role { get; init; }

    /// <summary>
    ///     Книги пользователя.
    /// </summary>
    public ICollection<BookEntity>? Books { get; init; }
}