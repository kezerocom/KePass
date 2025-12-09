using KePass.Server.Models;

namespace KePass.Server.Services.Definitions;

public interface ISubscriptionService
{
    Task<IEnumerable<Subscription>> GetSubscriptionsByAccountAsync(Guid accountId);
    Task<bool> IsWithinStorageLimitAsync(Guid accountId, Guid subscriptionId, long additionalBytes);
    Task UpgradeSubscriptionAsync(Guid accountId, Subscription subscription);
    Task CancelSubscriptionAsync(Guid accountId, Guid subscriptionId);
}