using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Domain.Entities;

/// <summary>
///     Базовая сущность.
/// </summary>
public abstract record BaseEntity
{
    /// <summary>
    ///     Уникальный идентификатор.
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; init; }
}