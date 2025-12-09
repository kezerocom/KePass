using KePass.Server.Models;
using KePass.Server.Types;

namespace KePass.Server.Services.Definitions;

public interface IAttachmentService
{
    Task<Attachment> AddAttachmentAsync(Guid vaultId, Blob blob);
    Task<Attachment?> GetAttachmentAsync(Guid attachmentId);
    Task<IEnumerable<Attachment>> GetAttachmentsByVaultAsync(Guid vaultId);
    Task RemoveAttachmentAsync(Guid attachmentId);
    Task UpdateAttachmentAsync(Attachment attachment);
    Task<long> GetTotalStorageUsedAsync(Guid accountId);
}