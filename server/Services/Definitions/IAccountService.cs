using KePass.Server.Models;
using KePass.Server.ValueObjects;
using KePass.Server.Commons;

namespace KePass.Server.Services.Definitions;

public interface IAccountService
{
    Task<OperationResult<Account>> GetByIdAsync(Guid id);
    Task<OperationResult<Account>> GetByUsernameAsync(string username);
    Task<OperationResult<Account>> GetByEmailAsync(Email email);
    Task<OperationResult<Account>> CreateAsync(string username, Email email, Password password);
    Task<OperationResult<Account>> UpdateUsernameAsync(Guid id, string newUsername);
    Task<OperationResult<Account>> UpdateEmailAsync(Guid id, Email newEmail);
    Task<OperationResult<Account>> UpdatePasswordAsync(Guid id, Password newPassword);
    Task<OperationResult<Account>> ActivateAsync(Guid id);
    Task<OperationResult<Account>> DeactivateAsync(Guid id);
}