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
    opts.DisableNpgsqlLogging = true;
    opts.Schema.For<ShoppingTrolley>().Identity(s => s.Username);
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

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//  HTTP pipeline here
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
