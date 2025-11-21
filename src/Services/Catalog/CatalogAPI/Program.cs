using BuildingBlocks.Behavior;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
//Add services to the container.
builder.Services.AddMediatR(confug =>
{
    confug.RegisterServicesFromAssembly(assembly);
    confug.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exceptionHandlerPathFeature == null)
            return;

        var problemDetails = new ProblemDetails
        {
            Title = exceptionHandlerPathFeature.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exceptionHandlerPathFeature.Message
        };

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exceptionHandlerPathFeature, exceptionHandlerPathFeature.Message);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});

app.Run();
