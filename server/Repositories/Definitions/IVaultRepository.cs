using KePass.Server.Models;

namespace KePass.Server.Repositories.Definitions;

public interface IVaultRepository
{
    Task<Vault?> GetByIdAsync(Guid vaultId);
    Task<IEnumerable<Vault>> GetByAccountIdAsync(Guid accountId);
    Task AddAsync(Vault vault);
    Task UpdateAsync(Vault vault);
    Task SetActiveAsync(Guid vaultId);
}