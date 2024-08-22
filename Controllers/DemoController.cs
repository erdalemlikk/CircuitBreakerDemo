using CircuitBreakerDemo.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CircuitBreakerDemo.Controllers;

[ApiController]
[Route(("[controller]"))]
public class DemoController : ControllerBase
{

    [HttpGet("NotFoundException")]
    public async Task<JsonResult> NotFoundException()
    {
        throw new DataNotFoundException();

    }
    
    [HttpGet("UnprocessableRequestException")]
    public async Task<JsonResult> UnprocessableRequestException()
    {
        throw new UnprocessableRequestException("Request is unprocessable");

    }
    
    [HttpGet("UnhandledException")]
    public async Task<JsonResult> UnhandledException()
    {
        throw new TimeoutException("Request is timeout");

    }

    [HttpGet("Successful")]
    public async Task<JsonResult> Successful()
    {
        return new JsonResult(Ok("Request Successful"));
    }
}