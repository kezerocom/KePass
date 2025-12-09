namespace KePass.Server.Types.Definitions;

public interface IValidation
{
    public bool IsValid();
    public bool IsValid<T>(T value);
}