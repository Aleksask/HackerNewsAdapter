using System.Reflection;
using System.Threading.RateLimiting;
using HackerNewsAdapter;
using HackerNewsAdapter.Api;
using HackerNewsAdapter.Client;
using Mapster;
using Microsoft.AspNetCore.RateLimiting;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;
    options.AddConcurrencyLimiter(Constants.RateLimiterPolicy, concurrencyOptions =>
        {
            concurrencyOptions.PermitLimit = 10;
            concurrencyOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            concurrencyOptions.QueueLimit = 100;
        });
});

builder.Services.AddSingleton<IHackerNewsApiClient>(_ =>
    RestService.For<IHackerNewsApiClient>("https://hacker-news.firebaseio.com"));

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly()!);

var app = builder.Build();
app.UseRateLimiter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

HackerNewsAdapterApi.Register(app);

app.Run();
