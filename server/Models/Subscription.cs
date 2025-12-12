using KePass.Server.ValueObjects.Enums;

namespace KePass.Server.Models;

public class Subscription
{
    public required Guid Id { get; set; }
    public required Guid AccountId { get; set; }
    public required long Storage { get; set; }
    public required string PaymentId { get; set; }
    public required SubscriptionType Name { get; set; }
    public required bool IsActive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required DateTime ExpireAt { get; set; }
}