using CircuitBreakerDemo.Filters;
using CircuitBreakerDemo.Middlewares;
using CircuitBreakerDemo.Middlewares.CircuitBreaker;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
var app = builder.Build();

app.UseCircuitBreaker(CircuitBreakerPolicy.GetCircuitBreakerPolicy());
app.MapControllers();
app.Run();