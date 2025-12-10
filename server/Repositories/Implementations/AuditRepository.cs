using System.Linq.Expressions;
using KePass.Server.Data;
using KePass.Server.Models;
using KePass.Server.Repositories.Definitions;
using Microsoft.EntityFrameworkCore;

namespace KePass.Server.Repositories.Implementations;

public class AuditRepository(DatabaseContext context) : IRepository<Audit>
{
    public async Task<Audit?> GetAsync(Expression<Func<Audit, bool>> predicate)
    {
        return await context.Audits.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<Audit?> GetByIdAsync(Guid id)
    {
        return await context.Audits.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Audit>> GetAllAsync()
    {
        return await context.Audits.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Audit>> GetAllAsync(Expression<Func<Audit, bool>> predicate)
    {
        return await context.Audits.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Audit>> ExecuteAsync(Func<IQueryable<Audit>, IQueryable<Audit>> query)
    {
        return await query(context.Audits.AsNoTracking()).ToListAsync();
    }

    public async Task<Audit?> AddAsync(Audit entity)
    {
        try
        {
            await context.Audits.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Audit?> UpdateAsync(Audit entity)
    {
        try
        {
            context.Audits.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Audit?> DeleteAsync(Audit entity)
    {
        try
        {
            context.Audits.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }
}