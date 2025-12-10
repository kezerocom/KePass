using System.Linq.Expressions;
using KePass.Server.Data;
using KePass.Server.Models;
using KePass.Server.Repositories.Definitions;
using Microsoft.EntityFrameworkCore;

namespace KePass.Server.Repositories.Implementations;

public class SubscriptionRepository(DatabaseContext context) : IRepository<Subscription>
{
    public async Task<Subscription?> GetByIdAsync(Guid id)
    {
        return await context.Subscriptions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        return await context.Subscriptions.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync(Expression<Func<Subscription, bool>> predicate)
    {
        return await context.Subscriptions.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> ExecuteAsync(
        Func<IQueryable<Subscription>, IQueryable<Subscription>> query)
    {
        return await query(context.Subscriptions.AsNoTracking()).ToListAsync();
    }

    public async Task<Subscription?> AddAsync(Subscription entity)
    {
        try
        {
            await context.Subscriptions.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Subscription?> UpdateAsync(Subscription entity)
    {
        try
        {
            context.Subscriptions.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Subscription?> DeleteAsync(Subscription entity)
    {
        try
        {
            context.Subscriptions.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }
}