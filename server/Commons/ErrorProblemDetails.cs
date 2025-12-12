namespace KePass.Server.Commons;

public class ErrorProblemDetails(string detail)
{
    public string Detail { get; set; } = detail;
}