using System.Linq.Expressions;
using KePass.Server.Data;
using KePass.Server.Models;
using KePass.Server.Repositories.Definitions;
using Microsoft.EntityFrameworkCore;

namespace KePass.Server.Repositories.Implementations;

public class AttachmentRepository(DatabaseContext context) : IRepository<Attachment>
{
    public async Task<Attachment?> GetAsync(Expression<Func<Attachment, bool>> predicate)
    {
        return await context.Attachments.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<Attachment?> GetByIdAsync(Guid id)
    {
        return await context.Attachments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Attachment>> GetAllAsync()
    {
        return await context.Attachments.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Attachment>> GetAllAsync(Expression<Func<Attachment, bool>> predicate)
    {
        return await context.Attachments.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Attachment>> ExecuteAsync(Func<IQueryable<Attachment>, IQueryable<Attachment>> query)
    {
        return await query(context.Attachments.AsNoTracking()).ToListAsync();
    }

    public async Task<Attachment?> AddAsync(Attachment entity)
    {
        try
        {
            await context.Attachments.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Attachment?> UpdateAsync(Attachment entity)
    {
        try
        {
            context.Attachments.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Attachment?> DeleteAsync(Attachment entity)
    {
        try
        {
            context.Attachments.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }
}