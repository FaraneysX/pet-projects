using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Domain.Entities;

/// <summary>
///     Сущность: книга.
/// </summary>
[Table("book")]
public record BookEntity : BaseEntity
{
    /// <summary>
    ///     Название.
    /// </summary>
    [Column("title")]
    public required string Title { get; set; }

    /// <summary>
    ///     Автор.
    /// </summary>
    [Column("author")]
    public required string Author { get; set; }

    /// <summary>
    ///     Владелец книги.
    /// </summary>
    public UserEntity? User { get; set; }
}