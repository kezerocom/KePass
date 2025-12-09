namespace KePass.Server.Services.Definitions;

public interface IEnvironmentService
{
    string? Get(string key);
    string? Get(string key, string? defaultValue);
    void Set(string key, string? value);
}