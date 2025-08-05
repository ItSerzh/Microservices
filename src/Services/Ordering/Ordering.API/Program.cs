
var builder = WebApplication.CreateBuilder(args);

// services here
builder.Services.AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

//  HTTP pipeline here
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabseAsync();
}

app.Run();
