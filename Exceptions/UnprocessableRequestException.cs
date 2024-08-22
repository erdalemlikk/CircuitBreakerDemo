using System.Net;

namespace CircuitBreakerDemo.Exceptions;

public class UnprocessableRequestException : Exception
{
    public UnprocessableRequestException(string message):base(message)
    {
        StatusCode = HttpStatusCode.UnprocessableEntity;
    }
    
    public HttpStatusCode StatusCode { get; private set; }
}