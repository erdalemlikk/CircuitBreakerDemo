using CircuitBreakerDemo.Middlewares.CircuitBreaker;
using Polly.CircuitBreaker;

namespace CircuitBreakerDemo.Middlewares;

public static class MiddlewareExtensions
{ 
    public static IApplicationBuilder UseCircuitBreaker(this IApplicationBuilder builder,AsyncCircuitBreakerPolicy policy)
    {
        
        return builder.UseMiddleware<CircuitBreakerMiddleware>(policy);
    }
}