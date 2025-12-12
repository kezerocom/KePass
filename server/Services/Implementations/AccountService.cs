using KePass.Server.Commons;
using KePass.Server.Models;
using KePass.Server.Repositories.Definitions;
using KePass.Server.Services.Definitions;
using KePass.Server.ValueObjects;
using KePass.Server.ValueObjects.Enums;

namespace KePass.Server.Services.Implementations;

public class AccountService(IRepositoryBase<Account> repositoryBase) : IAccountService
{
    public async Task<OperationResult<Account>> GetByIdAsync(Guid id)
    {
        var account = await repositoryBase.GetByIdAsync(id);

        return account != null
            ? OperationResult<Account>.Ok(account)
            : OperationResult<Account>.Fail($"Account with ID '{id}' not found.");
    }

    public async Task<OperationResult<Account>> GetByUsernameAsync(string username)
    {
        var account = await repositoryBase.GetAsync(a => a.Username.ToLower().Trim() == username.ToLower().Trim());

        return account != null
            ? OperationResult<Account>.Ok(account)
            : OperationResult<Account>.Fail($"Account with username '{username}' not found.");
    }

    public async Task<OperationResult<Account>> GetByEmailAsync(Email email)
    {
        var account = await repositoryBase.GetAsync(a => a.Email.Value.ToLower().Trim() == email.Value.ToLower().Trim());

        return account != null
            ? OperationResult<Account>.Ok(account)
            : OperationResult<Account>.Fail($"Account with email '{email.Value}' not found.");
    }

    public async Task<OperationResult<Account>> CreateAsync(string username, Email email, Password password,
        AccountRole role)
    {
        if (!email.IsValid())
            return OperationResult<Account>.Fail($"Invalid email '{email.Value}'.");

        if (!password.IsValid())
            return OperationResult<Account>.Fail("Password does not meet required criteria.");

        if (await GetByUsernameAsync(username) is { Success: true })
            return OperationResult<Account>.Fail($"Username '{username}' is already in use.");

        if (await GetByEmailAsync(email) is { Success: true })
            return OperationResult<Account>.Fail($"Email '{email.Value}' is already in use.");

        var account = new Account
        {
            Id = Guid.CreateVersion7(),
            Username = username.ToLower().Trim(),
            Email = email,
            Password = password,
            IsActive = true,
            Role = role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (!account.IsValid())
            return OperationResult<Account>.Fail("Created account entity is invalid.");

        var created = await repositoryBase.AddAsync(account);

        return created != null
            ? OperationResult<Account>.Ok(created)
            : OperationResult<Account>.Fail("Failed to create account due to an internal error.");
    }

    public async Task<OperationResult<Account>> UpdateUsernameAsync(Guid id, string newUsername)
    {
        if (await GetByUsernameAsync(newUsername) is { Success: true })
            return OperationResult<Account>.Fail($"Username '{newUsername}' is already in use.");

        var accountResult = await GetByIdAsync(id);
        if (!accountResult.Success) return accountResult;

        var account = accountResult.Result;
        account.Username = newUsername.ToLower().Trim();

        return await UpdateAccountAsync(account);
    }

    public async Task<OperationResult<Account>> UpdateEmailAsync(Guid id, Email newEmail)
    {
        if (!newEmail.IsValid())
            return OperationResult<Account>.Fail($"Invalid email '{newEmail.Value}'.");

        if (await GetByEmailAsync(newEmail) is { Success: true })
            return OperationResult<Account>.Fail($"Email '{newEmail.Value}' is already in use.");

        var accountResult = await GetByIdAsync(id);
        if (!accountResult.Success) return accountResult;

        var account = accountResult.Result;
        account.Email = newEmail;

        return await UpdateAccountAsync(account);
    }

    public async Task<OperationResult<Account>> UpdatePasswordAsync(Guid id, Password newPassword)
    {
        if (!newPassword.IsValid())
            return OperationResult<Account>.Fail("Invalid password.");

        var accountResult = await GetByIdAsync(id);
        if (!accountResult.Success) return accountResult;

        var account = accountResult.Result;
        account.Password = newPassword;

        return await UpdateAccountAsync(account);
    }

    public async Task<OperationResult<Account>> UpdateRoleAsync(Guid id, AccountRole role)
    {
        var accountResult = await GetByIdAsync(id);
        if (!accountResult.Success) return accountResult;

        var account = accountResult.Result;
        account.Role = role;

        return await UpdateAccountAsync(account);
    }

    public async Task<OperationResult<Account>> ActivateAsync(Guid id)
    {
        var accountResult = await GetByIdAsync(id);
        if (!accountResult.Success) return accountResult;

        var account = accountResult.Result;
        account.IsActive = true;

        return await UpdateAccountAsync(account);
    }

    public async Task<OperationResult<Account>> DeactivateAsync(Guid id)
    {
        var accountResult = await GetByIdAsync(id);

        if (!accountResult.Success) return accountResult;

        var account = accountResult.Result;
        account.IsActive = false;

        return await UpdateAccountAsync(account);
    }

    private async Task<OperationResult<Account>> UpdateAccountAsync(Account account)
    {
        account.UpdatedAt = DateTime.UtcNow;

        if (!account.IsValid())
            return OperationResult<Account>.Fail("Account entity is invalid.");

        var updated = await repositoryBase.UpdateAsync(account);

        return updated != null
            ? OperationResult<Account>.Ok(updated)
            : OperationResult<Account>.Fail("Failed to update account.");
    }
}