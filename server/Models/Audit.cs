using KePass.Server.Types.Enums;

namespace KePass.Server.Models;

public class Audit
{
    public required Guid Id { get; set; }
    public required Guid AccountId { get; set; }
    public required Guid ResourceId { get; set; }
    public required AuditResourceType ResourceType { get; set; }
    public required string Action { get; set; }
    public required DateTime CreatedAt { get; set; }
}