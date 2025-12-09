using KePass.Server.Types.Definitions;
using KePass.Server.Types.Enums;

namespace KePass.Server.Types;

public class Key : IValidation
{
    public required KeyAlgorithm Algorithm { get; set; }
    public required byte[] PublicKey { get; set; }
    public required byte[] PrivateKey { get; set; }
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