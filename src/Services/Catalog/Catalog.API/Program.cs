using BuildingBlocks;
using BuildingBlocks.Behaviors;
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
app.Run();
