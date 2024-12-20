using GoldPrices.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddInfrastructure();
builder.AddLogging();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.EnsureDatabaseCreated();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AddMiddleware();

app.Run();