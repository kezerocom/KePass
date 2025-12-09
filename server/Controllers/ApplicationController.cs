using System.Net;
using KePass.Server.Controllers.Definitions.Responses;
using KePass.Server.Services.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace KePass.Server.Controllers;

[ApiController]
[Route("/api/application")]
public class ApplicationController(IEnvironmentService environment) : ControllerBase
{
    [HttpGet("client")]
    [ProducesResponseType<ApplicationGetClientResponse>((int)HttpStatusCode.OK)]
    public IActionResult GetClient()
    {
        var name = environment.Get("APPLICATION_CLIENT_NAME", "KePass")!;
        var version = Version.Parse(environment.Get("APPLICATION_CLIENT_VERSION", new Version(1, 0).ToString())!);

        return Ok(new ApplicationGetClientResponse(name, version));
    }

    [HttpGet("server")]
    [ProducesResponseType<ApplicationGetServerResponse>((int)HttpStatusCode.OK)]
    public IActionResult GetServer()
    {
        var name = environment.Get("APPLICATION_SERVER_NAME", GetType().Assembly.GetName().Name ?? "Unknown")!;
        var instance = environment.Get("APPLICATION_SERVER_INSTANCE", Environment.MachineName)!;
        var version = Version.Parse(environment.Get("APPLICATION_SERVER_VERSION", new Version(1, 0).ToString())!);

        return Ok(new ApplicationGetServerResponse(name, version, instance));
    }
}