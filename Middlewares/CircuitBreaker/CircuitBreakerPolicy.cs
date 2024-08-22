using CircuitBreakerDemo.Exceptions;
using Polly.CircuitBreaker;
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
                    Console.WriteLine($"Circuit breaker opened for {breakDelay.TotalSeconds} seconds due to: {ex.Message}");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit breaker reset.");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine("Circuit breaker is half-open.");
                });
    }
}