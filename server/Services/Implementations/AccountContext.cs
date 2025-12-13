using KePass.Server.Commons;
using KePass.Server.Services.Definitions;

namespace KePass.Server.Services.Implementations;

public class AccountContext(IHttpContextAccessor accessor) : IAccountContext
{
    public bool IsExistent => Current != null;
    public bool IsNotExistent => Current == null;
    public CurrentAccount? Current { get; } = CurrentAccount.CreateFromClaims(accessor.HttpContext?.User.Claims.ToArray());
}