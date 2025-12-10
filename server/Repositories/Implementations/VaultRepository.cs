using System.Linq.Expressions;
using KePass.Server.Data;
using KePass.Server.Models;
using KePass.Server.Repositories.Definitions;
using Microsoft.EntityFrameworkCore;

namespace KePass.Server.Repositories.Implementations;

public class VaultRepository(DatabaseContext context) : IRepository<Vault>
{
    public async Task<Vault?> GetAsync(Expression<Func<Vault, bool>> predicate)
    {
        return await context.Vaults.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<Vault?> GetByIdAsync(Guid id)
    {
        return await context.Vaults.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Vault>> GetAllAsync()
    {
        return await context.Vaults.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Vault>> GetAllAsync(Expression<Func<Vault, bool>> predicate)
    {
        return await context.Vaults.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Vault>> ExecuteAsync(Func<IQueryable<Vault>, IQueryable<Vault>> query)
    {
        return await query(context.Vaults.AsNoTracking()).ToListAsync();
    }

    public async Task<Vault?> AddAsync(Vault entity)
    {
        try
        {
            await context.Vaults.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Vault?> UpdateAsync(Vault entity)
    {
        try
        {
            context.Vaults.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Vault?> DeleteAsync(Vault entity)
    {
        try
        {
            context.Vaults.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }
}