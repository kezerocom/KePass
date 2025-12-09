using KePass.Server.Models;

namespace KePass.Server.Repositories.Definitions;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid accountId);
    Task<Account?> GetByUsernameAsync(string username);
    Task<Account?> GetByEmailAsync(string email);
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
    Task SetActiveAsync(Guid accountId, bool isActive);
}