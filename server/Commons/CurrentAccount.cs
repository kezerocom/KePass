using System.Security.Claims;
using KePass.Server.Models;
using KePass.Server.ValueObjects.Enums;

namespace KePass.Server.Commons;

[Serializable]
public class CurrentAccount
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required AccountRole Role { get; set; }


    public static CurrentAccount? CreateFromAccount(Account? account)
    {
        try
        {
            if (account == null) return null;

            return new CurrentAccount
            {
                Id = account!.Id,
                Username = account.Username,
                Email = account.Email.ToString(),
                Role = account.Role,
            };
        }
        catch
        {
            return null;
        }
    }

    public static CurrentAccount? CreateFromClaims(Claim[]? claims)
    {
        try
        {
            if (claims == null || claims.Length == 0) return null;

            return new CurrentAccount
            {
                Id = Guid.Parse(claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value),
                Username = claims.First(x => x.Type == ClaimTypes.Name).Value,
                Email = claims.First(x => x.Type == ClaimTypes.Email).Value,
                Role = Enum.Parse<AccountRole>(claims.First(x => x.Type == ClaimTypes.Role).Value, true),
            };
        }
        catch
        {
            return null;
        }
    }

    public Claim[] ToClaims()
    {
        return
        [
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Name, Username),
            new Claim(ClaimTypes.Email, Email),
            new Claim(ClaimTypes.Role, Role.ToString())
        ];
    }
}