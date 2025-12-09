namespace KePass.Server.Services.Definitions;

public interface IEnvironmentService
{
    string Get(string key);
    T GetValue<T>(string key);
    bool IsDevelopment();
    bool IsProduction();
}