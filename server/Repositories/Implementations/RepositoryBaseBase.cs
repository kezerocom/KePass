using System.Linq.Expressions;
using KePass.Server.Commons.Definitions;
using KePass.Server.Data;
using KePass.Server.Repositories.Definitions;
using Microsoft.EntityFrameworkCore;

namespace KePass.Server.Repositories.Implementations;

public class RepositoryBaseBase<TEntity>(DatabaseContext context) : IRepositoryBase<TEntity> where TEntity : class, IEntity
{
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ExecuteAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query)
    {
        return await query(context.Set<TEntity>().AsNoTracking()).ToListAsync();
    }

    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        try
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        try
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<TEntity?> DeleteAsync(TEntity entity)
    {
        try
        {
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }
}