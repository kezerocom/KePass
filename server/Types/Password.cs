using KePass.Server.Types.Definitions;
using KePass.Server.Types.Enums;

namespace KePass.Server.Types;

public class Password : IValidation
{
    public required byte[] Hash { get; set; }
    public required byte[] Salt { get; set; }
    public required uint Memory { get; set; }
    public required PasswordAlgorithm Algorithm { get; set; }
    public required uint Iterations { get; set; }
    public required uint Parallelism { get; set; }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public bool IsValid<T>(T value)
    {
        throw new NotImplementedException();
    }
}