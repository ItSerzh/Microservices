
namespace ShoppingCart.API.Basket.CreateShoppingCart;

public record CreateShoppingCartRequest(ShoppingTrolley ShoppingTrolley);

public record CreateShoppingCartResponse(string Username);

public class CreateShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/shopping-cart", async (CreateShoppingCartRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<CreateShoppingCartCommand>();

            var result = await mediator.Send(command);

            var response = result.Adapt<CreateShoppingCartResponse>();

            return Results.Created($"/shopping-cart/{response.Username}", response);
        })
            .WithName("CreateShoppingCart")
            .Produces<CreateShoppingCartResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDisplayName("Create Shopping Cart")
            .WithDescription("Create Shopping Cart");
    }
}
