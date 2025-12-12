using System.Linq.Expressions;
using KePass.Server.Commons.Definitions;

namespace KePass.Server.Repositories.Definitions;

public interface IRepositoryBase<T> where T : IEntity
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> ExecuteAsync(Func<IQueryable<T>, IQueryable<T>> query);

    Task<T?> AddAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<T?> DeleteAsync(T entity);
}