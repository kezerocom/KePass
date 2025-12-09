namespace KePass.Server.Services.Definitions;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}