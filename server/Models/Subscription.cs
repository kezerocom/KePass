using KePass.Server.Commons.Definitions;
using KePass.Server.ValueObjects.Enums;

namespace KePass.Server.Models;

public class Subscription : IValidation
{
    public required Guid Id { get; set; }
    public required Guid AccountId { get; set; }
    public required string PaymentId { get; set; }
    public required long Storage { get; set; }
    public required SubscriptionType Type { get; set; }
    public required bool IsActive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required DateTime ExpireAt { get; set; }

    public bool IsValid()
    {
        return
            Id != Guid.Empty &&
            AccountId != Guid.Empty &&
            !string.IsNullOrEmpty(PaymentId) &&
            Storage > 0 &&
            CreatedAt.Kind == DateTimeKind.Utc &&
            UpdatedAt.Kind == DateTimeKind.Utc &&
            ExpireAt.Kind == DateTimeKind.Utc &&
            CreatedAt >= DateTime.UnixEpoch &&
            UpdatedAt >= CreatedAt &&
            ExpireAt >= CreatedAt;
    }
}