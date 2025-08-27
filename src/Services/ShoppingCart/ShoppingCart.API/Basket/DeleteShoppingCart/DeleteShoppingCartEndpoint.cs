
namespace ShoppingCart.API.Basket.DeleteShoppingCart;

//public record DeleteShoppingCartRequest(string Username);

public record DeleteShoppingCartResponce(bool IsSuccess);

public class DeleteShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/shopping-cart/{userId}", async (Guid userId, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteShoppingCartCommand(userId));

            var response = result.Adapt<DeleteShoppingCartResponce>();

            return Results.Ok(response);
        })
            .WithName("DeleteShopingCart")
            .Produces<DeleteShoppingCartResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDisplayName("Delete Shopping Cart")
            .WithDescription("Delete Shopping Cart");
    }
}
