using System.Linq.Expressions;

namespace KePass.Server.Services.Definitions;

public interface IPersistenceService
{
    Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
    Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>>? predicate = null) where T : class;
    Task AddAsync<T>(T entity) where T : class;
    Task UpdateAsync<T>(T entity) where T : class;
    Task DeleteAsync<T>(T entity) where T : class;
    Task<int> SaveChangesAsync();
}