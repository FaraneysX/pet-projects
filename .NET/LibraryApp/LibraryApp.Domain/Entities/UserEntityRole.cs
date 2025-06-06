using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Domain.Entities;

/// <summary>
///     Роль сущности: пользователь.
/// </summary>
[Table("user_role")]
public record UserEntityRole : BaseEntity
{
    /// <summary>
    ///     Название.
    /// </summary>
    [Column("name")]
    public required string Name { get; init; }

    /// <summary>
    ///     Список пользователей, относящихся к данной роли.
    /// </summary>
    public ICollection<UserEntity>? Users { get; init; }
}