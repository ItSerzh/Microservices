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
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingTrolley>().Identity(s => s.Username);
    opts.DisableNpgsqlLogging = true;
}).UseLightweightSessions();

var app = builder.Build();

//  HTTP pipeline here
app.MapCarter();

app.Run();
