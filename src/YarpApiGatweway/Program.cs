using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));  

builder.Services.AddRateLimiter(rateLimitOptions =>
{
    rateLimitOptions.AddFixedWindowLimiter("fixed", options =>
        {
            options.Window = TimeSpan.FromSeconds(10); // Time window duration
            options.PermitLimit = 5; // Maximum number of requests allowed in the time window
        });
});

var app = builder.Build();


//Configure the HTTP request pipeline.
app.UseRateLimiter(); // Enable rate limiting middleware

app.MapReverseProxy();

app.Run();
