using Polly.CircuitBreaker;
using Serilog;

namespace CircuitBreakerDemo.Middlewares.CircuitBreaker;

public class CircuitBreakerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

    public CircuitBreakerMiddleware(RequestDelegate next, AsyncCircuitBreakerPolicy circuitBreakerPolicy)
    {
        _next = next;
        _circuitBreakerPolicy = circuitBreakerPolicy;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _circuitBreakerPolicy.ExecuteAsync(async () =>
            {
                await _next(context);
            });
        }
        catch (BrokenCircuitException)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Service is temporarily unavailable due to high failure rate.");
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("An unexpected error occurred.");
            Log.Error($"Unexpected error: {ex.Message}");
        }
    }
}