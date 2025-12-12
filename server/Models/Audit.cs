using KePass.Server.Commons.Definitions;
using KePass.Server.ValueObjects.Enums;

namespace KePass.Server.Models;

public class Audit : IEntity, IValidation
{
    public required Guid Id { get; set; }
    public required Guid AccountId { get; set; }
    public required Guid ResourceId { get; set; }
    public required AuditResourceType ResourceType { get; set; }
    public required string Action { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }

    public bool IsValid()
    {
        return
            Id != Guid.Empty &&
            AccountId != Guid.Empty &&
            ResourceId != Guid.Empty &&
            !string.IsNullOrWhiteSpace(Action) &&
            CreatedAt.Kind == DateTimeKind.Utc &&
            UpdatedAt.Kind == DateTimeKind.Utc &&
            CreatedAt > DateTime.UnixEpoch &&
            UpdatedAt >= CreatedAt;
    }
}