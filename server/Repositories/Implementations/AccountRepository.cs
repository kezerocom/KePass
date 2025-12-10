using System.Linq.Expressions;
using KePass.Server.Data;
using KePass.Server.Models;
using KePass.Server.Repositories.Definitions;
using Microsoft.EntityFrameworkCore;

namespace KePass.Server.Repositories.Implementations;

public class AccountRepository(DatabaseContext context) : IRepository<Account>
{
    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await context.Accounts.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Account>> GetAllAsync(Expression<Func<Account, bool>> predicate)
    {
        return await context.Accounts.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Account>> ExecuteAsync(Func<IQueryable<Account>, IQueryable<Account>> query)
    {
        return await query(context.Accounts.AsNoTracking()).ToListAsync();
    }

    public async Task<Account?> AddAsync(Account entity)
    {
        try
        {
            await context.Accounts.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Account?> UpdateAsync(Account entity)
    {
        try
        {
            context.Accounts.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Account?> DeleteAsync(Account entity)
    {
        try
        {
            context.Accounts.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }
}