using KePass.Server.Models;
using KePass.Server.ValueObjects.Enums;

namespace KePass.Server.Services.Definitions;

public interface IAuditService
{
    Task LogAsync(Guid accountId, Guid resourceId, AuditResourceType resourceType, string action);
    Task<IEnumerable<Audit>> GetByResourceAsync(Guid resourceId, AuditResourceType resourceType);
    Task<IEnumerable<Audit>> GetByAccountAsync(Guid accountId);
}