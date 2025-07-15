var builder = WebApplication.CreateBuilder(args);
// services here

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//  HTTP pipeline here

app.Run();
