using KePass.Server.Models;

namespace KePass.Server.Repositories.Definitions;

public interface IAttachmentRepository
{
    Task<Attachment?> GetByIdAsync(Guid attachmentId);
    Task<IEnumerable<Attachment>> GetByVaultIdAsync(Guid vaultId);
    Task AddAsync(Attachment attachment);
    Task UpdateAsync(Attachment attachment);
    Task SetActiveAsync(Guid attachmentId);
    Task<long> GetTotalStorageByAccountAsync(Guid accountId);
}