using KePass.Server.Types.Definitions;

namespace KePass.Server.Types;

public class Blob : IValidation
{
    public required byte[] Value { get; set; }
    public required long Size { get; set; }
    public required bool IsEncrypted { get; set; }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public bool IsValid<T>(T value)
    {
        throw new NotImplementedException();
    }
}