using System.Linq.Expressions;
using KePass.Server.Data;
using KePass.Server.Models;
using KePass.Server.Repositories.Definitions;
using Microsoft.EntityFrameworkCore;

namespace KePass.Server.Repositories.Implementations;

public class BlobRepository(DatabaseContext context) : IRepository<Blob>
{
    public async Task<Blob?> GetAsync(Expression<Func<Blob, bool>> predicate)
    {
        return await context.Blobs.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<Blob?> GetByIdAsync(Guid id)
    {
        return await context.Blobs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Blob>> GetAllAsync()
    {
        return await context.Blobs.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Blob>> GetAllAsync(Expression<Func<Blob, bool>> predicate)
    {
        return await context.Blobs.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Blob>> ExecuteAsync(Func<IQueryable<Blob>, IQueryable<Blob>> query)
    {
        return await query(context.Blobs.AsNoTracking()).ToListAsync();
    }

    public async Task<Blob?> AddAsync(Blob entity)
    {
        try
        {
            await context.Blobs.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Blob?> UpdateAsync(Blob entity)
    {
        try
        {
            context.Blobs.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Blob?> DeleteAsync(Blob entity)
    {
        try
        {
            context.Blobs.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }
}