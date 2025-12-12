using KePass.Server.Commons.Definitions;
using KePass.Server.ValueObjects;

namespace KePass.Server.Models;

public class Attachment : IEntity, IValidation
{
    public required Guid Id { get; set; }
    public required Guid Name { get; set; }
    public required Guid VaultId { get; set; }
    public required Guid BlobId { get; set; }
    public required bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public bool IsValid()
    {
        return
            Id != Guid.Empty &&
            Name != Guid.Empty &&
            VaultId != Guid.Empty &&
            BlobId != Guid.Empty &&
            CreatedAt.Kind == DateTimeKind.Utc &&
            UpdatedAt.Kind == DateTimeKind.Utc &&
            CreatedAt > DateTime.UnixEpoch &&
            UpdatedAt >= CreatedAt;
    }
}