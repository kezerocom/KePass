using KePass.Server.Types;

namespace KePass.Server.Models;

public class Vault
{
    public required Guid Id { get; set; }
    public required Guid AccountId { get; set; }
    public required Key Key { get; set; }
    public required Version Version { get; set; }
    public required Blob Blob { get; set; }
    public required bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}