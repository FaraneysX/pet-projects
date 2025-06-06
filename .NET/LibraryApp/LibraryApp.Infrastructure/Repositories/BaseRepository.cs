using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Repositories;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories;

/// <summary>
///     Базовый репозиторий для работы с сущностями в базе данных.
/// </summary>
/// <param name="dbContext">
///     Контекст базы данных, используемый для выполнения операций с сущностями типа
///     <typeparamref name="TEntity" />.
/// </param>
/// <typeparam name="TEntity">
///     Тип сущности, с которой будет работать репозиторий. Этот тип должен наследовать от
///     <see cref="BaseEntity" />.
/// </typeparam>
public class BaseRepository<TEntity>(ApplicationDbContext dbContext) : IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Контекст базы данных.
    /// </summary>
    protected ApplicationDbContext DbContext { get; } = dbContext;

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await DbContext.Set<TEntity>().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetById(int id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<TEntity?> Save(TEntity entity)
    {
        var existingEntity = await GetById(entity.Id);

        if (existingEntity is null)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            await SaveChanges();

            return await GetById(entity.Id);
        }

        DbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        await SaveChanges();

        return existingEntity;
    }

    /// <inheritdoc />
    public async Task Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);

        await SaveChanges();
    }

    /// <summary>
    ///     Сохранение изменений в базе данных.
    /// </summary>
    protected async Task SaveChanges()
    {
        await DbContext.SaveChangesAsync();
    }
}