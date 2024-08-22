using System.Net;

namespace CircuitBreakerDemo.Exceptions;

public class DataNotFoundException : Exception
{
    public DataNotFoundException()
    {
        StatusCode = HttpStatusCode.NoContent;
    }
    public HttpStatusCode StatusCode { get; private set; }
}