var builder = WebApplication.CreateBuilder(args);

// services here
builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//  HTTP pipeline here

app.Run();
