using KePass.Server.Commons.Definitions;

namespace KePass.Server.Models;

public class Blob : IValidation
{
    public required Guid Id { get; set; }
    public required Guid Name { get; set; }
    public required long Size { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public bool IsValid()
    {
        return
            Id != Guid.Empty &&
            Name != Guid.Empty &&
            Size > 0 &&
            CreatedAt.Kind == DateTimeKind.Utc &&
            UpdatedAt.Kind == DateTimeKind.Utc &&
            CreatedAt > DateTime.UnixEpoch &&
            UpdatedAt >= CreatedAt;
    }
}