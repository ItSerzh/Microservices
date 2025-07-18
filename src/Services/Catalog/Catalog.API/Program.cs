using BuildingBlocks;
using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// services here
var currentAssembly = Assembly.GetExecutingAssembly();
var dependencyCatalog = new DependencyContextAssemblyCatalog([
    currentAssembly,
    typeof(IBuildingBlocksMarker).Assembly]);

builder.Services.AddCarter(dependencyCatalog);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(currentAssembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(currentAssembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.DisableNpgsqlLogging = true;
}).UseLightweightSessions();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//  HTTP pipeline here
app.MapCarter();
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null) return;

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace
        };

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, exception.Message);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem + json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});
app.Run();
