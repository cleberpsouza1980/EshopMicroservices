using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;
using OrderingAPI;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
//Infrastructure - EF Core
//Application - MediatR
// API - Carter, HealthChecks, Swagger

//builder.services
// .AddApplicationServices()
// .AddInfrastructureServices(builder.Configuration)
// .AddWebServices();

builder.Services.AddApplicationServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddOrderingAPIServices();


var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
