using KePass.Server.Models;

namespace KePass.Server.Repositories.Definitions;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetByAccountIdAsync(Guid accountId);
    Task<Subscription?> GetByIdAsync(Guid subscriptionId);
    Task AddAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
    Task SetActiveAsync(Guid subscriptionId);
}