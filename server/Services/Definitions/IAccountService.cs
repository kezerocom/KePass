using KePass.Server.Models;
using KePass.Server.ValueObjects;
using KePass.Server.Commons;
using KePass.Server.ValueObjects.Enums;

namespace KePass.Server.Services.Definitions;

public interface IAccountService
{
    Task<OperationResult<Account>> GetByIdAsync(Guid id, ICurrentIdentity identity);
    Task<OperationResult<Account>> GetByUsernameAsync(string username, ICurrentIdentity identity);
    Task<OperationResult<Account>> GetByEmailAsync(Email email, ICurrentIdentity identity);
    Task<OperationResult<Account>> CreateAsync(string username, Email email, Password password, AccountRole role, ICurrentIdentity identity);
    Task<OperationResult<Account>> UpdateUsernameAsync(Guid id, string newUsername, ICurrentIdentity identity);
    Task<OperationResult<Account>> UpdateEmailAsync(Guid id, Email newEmail, ICurrentIdentity identity);
    Task<OperationResult<Account>> UpdatePasswordAsync(Guid id, Password newPassword, ICurrentIdentity identity);
    Task<OperationResult<Account>> UpdateRoleAsync(Guid id, AccountRole role, ICurrentIdentity identity);
    Task<OperationResult<Account>> ActivateAsync(Guid id, ICurrentIdentity identity);
    Task<OperationResult<Account>> DeactivateAsync(Guid id, ICurrentIdentity identity);
}