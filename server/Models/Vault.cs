using KePass.Server.Commons.Definitions;
using KePass.Server.ValueObjects;

namespace KePass.Server.Models;

public class Vault : IValidation
{
    public required Guid Id { get; set; }
    public required Guid AccountId { get; set; }
    public required Guid BlobId { get; set; }
    public required Key Key { get; set; }
    public required Version Version { get; set; }
    public required bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public bool IsValid()
    {
        return
            Id != Guid.Empty &&
            AccountId != Guid.Empty &&
            BlobId != Guid.Empty &&
            CreatedAt.Kind == DateTimeKind.Utc &&
            UpdatedAt.Kind == DateTimeKind.Utc &&
            CreatedAt >= DateTime.UnixEpoch &&
            UpdatedAt >= CreatedAt;
    }
}