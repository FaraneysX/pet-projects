using LibraryApp.Domain.Entities;

namespace LibraryApp.Domain.Repositories;

/// <summary>
///     Интерфейс репозитория.
/// </summary>
/// <typeparam name="TEntity">Тип сущности, производной от <see cref="BaseEntity" />.</typeparam>
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Получить все сущности типа <typeparamref name="TEntity" />.
    /// </summary>
    /// <returns>Коллекция всех сущностей типа <typeparamref name="TEntity" />.</returns>
    Task<IEnumerable<TEntity>> GetAll();

    /// <summary>
    ///     Получить сущность по ее уникальному идентификатору.
    /// </summary>
    /// <param name="id">Сущность типа <typeparamref name="TEntity" />, если найдена;<br />иначе <c>null</c>.</param>
    /// <returns></returns>
    Task<TEntity?> GetById(int id);

    /// <summary>
    ///     Сохранить сущность в репозитории.
    /// </summary>
    /// <param name="entity">Сущность, которую необходимо сохранить.</param>
    Task<TEntity?> Save(TEntity entity);

    /// <summary>
    ///     Удалить сущность из репозитория.
    /// </summary>
    /// <param name="entity">Сущность, которую необходимо удалить.</param>
    Task Delete(TEntity entity);
}