using BuildingBlocks.Extensions;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// services here
var currentAssembly = Assembly.GetExecutingAssembly();

//Application Services
builder.Services.AddCarterForAssembly(currentAssembly);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(currentAssembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Data Services
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.DisableNpgsqlLogging = true;
    // for now we use Id as identity and it works auotmatically
    //opts.Schema.For<ShoppingTrolley>().Identity(s => s.Username);
}).UseLightweightSessions();

builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
//builder.Services.AddScoped<IShoppingCartRepository>(provider =>
//{
//    var shoppingCartRepo = provider.GetRequiredService<ShoppingCartRepository>();
//    return new CachedShoppingCartRepository(shoppingCartRepo, provider.GetRequiredService<IDistributedCache>());
//});
builder.Services.Decorate<IShoppingCartRepository, CachedShoppingCartRepository>();

builder.Services.AddStackExchangeRedisCache(opts =>
{
    opts.Configuration = builder.Configuration.GetConnectionString("Redis");
    opts.InstanceName = currentAssembly.GetName().Name;
});

//Grpc services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opts =>
{
    opts.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        return handler;
    });

//Async communication services
builder.Services.AddMessageBroker(builder.Configuration);


//Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

//  HTTP pipeline here
app.MapCarter();

app.UseExceptionHandler(options => { });

app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
