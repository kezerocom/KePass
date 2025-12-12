using KePass.Server.Commons;
using KePass.Server.Services.Definitions;

namespace KePass.Server.Services.Implementations;

public class CurrentIdentity(IHttpContextAccessor accessor) : ICurrentIdentity
{
    public bool IsExistent => Identity != null;
    public bool IsNotExistent => Identity == null;
    public Identity? Identity => Identity.CreateFromClaims(accessor.HttpContext!.User.Claims.ToArray());
}