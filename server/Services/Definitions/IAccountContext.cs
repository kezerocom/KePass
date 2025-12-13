using KePass.Server.Commons;

namespace KePass.Server.Services.Definitions;

public interface IAccountContext
{
    bool IsExistent { get; }
    bool IsNotExistent { get; }
    CurrentAccount? Current { get; }
}