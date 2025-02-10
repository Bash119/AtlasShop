using Ordering.Application;
using Ordering.Infrastructure;
using OrderingAPI;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();


//----------------------------------
//Infrastrucutre - EF Core
//Application - MediatR
//API - Carter, HealthChecks, ....

//builder.Services
//    .AddApplicationServices()
//    .AddInfrastrucutreServices(builder.Configuration)
//    .AddWebServices();
//----------------------------------



var app = builder.Build();

//Configure the HTTP request pipeline

app.Run();
