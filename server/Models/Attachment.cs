using KePass.Server.Types;

namespace KePass.Server.Models;

public class Attachment
{
    public required Guid Id { get; set; }
    public required Guid VaultId { get; set; }
    public required Blob Blob { get; set; }
    public required bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}