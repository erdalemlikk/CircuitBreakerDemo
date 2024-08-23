using CircuitBreakerDemo.Exceptions;
using Polly.CircuitBreaker;
using Serilog;

namespace CircuitBreakerDemo.Middlewares.CircuitBreaker;

using Polly;

public static class CircuitBreakerPolicy
{
    public static AsyncCircuitBreakerPolicy GetCircuitBreakerPolicy()
    {
        return Policy
            .Handle<Exception>(ex => !(ex is DataNotFoundException or UnprocessableRequestException)) 
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 2,
                durationOfBreak: TimeSpan.FromSeconds(15),
                onBreak: (ex, breakDelay) =>
                {
                    Log.Information($"Circuit breaker opened for {breakDelay.TotalSeconds} seconds due to: {ex.Message}");
                },
                onReset: () =>
                {
                    Log.Information("Circuit breaker reset.");
                },
                onHalfOpen: () =>
                {
                    Log.Information("Circuit breaker is half-open.");
                });
    }
}