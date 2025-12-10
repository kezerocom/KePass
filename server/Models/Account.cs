using System.Text.RegularExpressions;
using KePass.Server.Types;
using KePass.Server.Types.Definitions;

namespace KePass.Server.Models;

public class Account : IValidation
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required Password Password { get; set; }
    public required Email Email { get; set; }
    public required bool IsActive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    
    public bool IsValid()
    {
        return
            Id != Guid.Empty &&
            Regex.IsMatch(Username, "^[a-z0-9]+(?:[.-]?[a-z0-9]+)*$") &&
            Password.IsValid() &&
            Email.IsValid() &&
            CreatedAt > DateTime.UnixEpoch &&
            UpdatedAt >= CreatedAt;
    }
}