using System.Reflection;
using FluentValidation;
using GoldPrices.Infrastructure.Behaviours;
using GoldPrices.Infrastructure.Middleware;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;

namespace GoldPrices.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<GoldPricesDbContext>();
        builder.Services.TryAddScoped<ApplicationDbContext>(provider => provider.GetService<GoldPricesDbContext>()!);

        builder.Services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return builder;
    }

    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        var serilogLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(serilogLogger);

        return builder;
    }

    public static WebApplication EnsureDatabaseCreated(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
            dbContext.Database.EnsureCreated();
        }

        return app;
    }

    public static WebApplication AddMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}