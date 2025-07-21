using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

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

builder.Services.AddValidatorsFromAssembly(currentAssembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.DisableNpgsqlLogging = true;
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

//  HTTP pipeline here
app.MapCarter();
app.UseExceptionHandler(exceptionHandlerApp =>{ });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();
