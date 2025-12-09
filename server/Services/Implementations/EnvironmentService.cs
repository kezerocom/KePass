using KePass.Server.Services.Definitions;

namespace KePass.Server.Services.Implementations;

public class EnvironmentService : IEnvironmentService
{
    public string? Get(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }

    public string? Get(string key, string? defaultValue)
    {
        return Environment.GetEnvironmentVariable(key) ?? defaultValue;
    }

    public void Set(string key, string? value)
    {
        Environment.SetEnvironmentVariable(key, value);
    }
}