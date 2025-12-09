namespace KePass.Server.Repositories.Definitions;

public interface IAuditRepository
{
    Task LogVaultChangeAsync(Guid vaultId, Guid accountId, string action);
    Task LogAttachmentChangeAsync(Guid attachmentId, Guid accountId, string action);
}