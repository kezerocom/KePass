using KePass.Server.Types.Definitions;

namespace KePass.Server.Types;

public class Email : IValidation
{
    public required string Value { get; set; }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public bool IsValid<T>(T value)
    {
        throw new NotImplementedException();
    }
}