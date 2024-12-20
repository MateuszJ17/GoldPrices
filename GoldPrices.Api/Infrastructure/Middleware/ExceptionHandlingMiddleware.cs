using FluentValidation;

namespace GoldPrices.Infrastructure.Middleware;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ValidationException validationException)
        {
            context.Response.StatusCode = 400;
            var validationErrors = validationException.Errors.ToList();

            await context.Response.WriteAsJsonAsync(validationErrors);
        }
        catch (Exception e)
        {
            logger.LogCritical(e.Message);
            throw;
        }
    }
}