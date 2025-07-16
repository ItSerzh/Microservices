using BuildingBlocks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// services here
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([
    Assembly.GetExecutingAssembly(),                       // Your main app
    typeof(IBuildingBlocksMarker).Assembly                 // External module assembly
]));
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//  HTTP pipeline here
app.MapCarter();
app.Run();
