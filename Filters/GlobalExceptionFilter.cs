using System.Net;
using CircuitBreakerDemo.Exceptions;
using CircuitBreakerDemo.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CircuitBreakerDemo.Filters;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
    
    public override Task OnExceptionAsync(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case UnprocessableRequestException unprocessableRequestException:
            {
                var (status, result) = UnprocessableRequestExceptionHandle(unprocessableRequestException);

                context.Result = new ObjectResult(result);
                context.HttpContext.Response.StatusCode = (int)status;
                break;
            }
            case DataNotFoundException dataNotFoundException:
            {
                context.Result = new NoContentResult();
                context.HttpContext.Response.StatusCode = (int)dataNotFoundException.StatusCode;
                break;
            }
        }

        return Task.CompletedTask;
    }
    private (HttpStatusCode, ErrorResponseModel) UnprocessableRequestExceptionHandle(
        UnprocessableRequestException exception)
    {
        return (exception.StatusCode, new ErrorResponseModel()
        {
            Message = exception.Message
        });
    }
}