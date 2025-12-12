using KePass.Server.Commons;

namespace KePass.Server.Services.Definitions;

public interface ICurrentIdentity
{
    bool IsExistent { get; }
    bool IsNotExistent { get; }
    Identity? Identity { get; }
}