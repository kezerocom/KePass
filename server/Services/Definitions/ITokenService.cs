using KePass.Server.Commons;
using KePass.Server.Models;

namespace KePass.Server.Services.Definitions;

public interface ITokenService
{
    string? Create(CurrentAccount? current);
    CurrentAccount? Parse(string? token);
}