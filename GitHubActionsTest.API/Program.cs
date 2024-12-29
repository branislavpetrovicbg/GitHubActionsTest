using GitHubActionsTest.API.Http;
using GitHubActionsTest.API.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddHttpClient<AirQualityHttpClient>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
    client.BaseAddress = new Uri(settings.Value.ExternalApiBaseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
