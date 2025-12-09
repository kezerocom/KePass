using KePass.Server.Types;

namespace KePass.Server.Models;

public class Account
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required Password Password { get; set; }
    public required Email Email { get; set; }
    public required bool IsActive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
}