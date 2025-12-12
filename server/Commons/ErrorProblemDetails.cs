namespace KePass.Server.Commons;

public class ErrorProblemDetails(string title, string detail)
{
    public string Title { get; set; } = title;
    public string Detail { get; set; } = detail;
}