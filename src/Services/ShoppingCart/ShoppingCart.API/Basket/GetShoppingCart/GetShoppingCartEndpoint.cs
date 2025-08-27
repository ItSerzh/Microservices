namespace ShoppingCart.API.Basket.GetShoppingCart;

public record GetShoppingCartRequest(string Username );

public record GetShoppingCartResponse(ShoppingTrolley ShoppingTrolley);

public class GetShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shopping-cart/{userId}", async (Guid userId, ISender sender) =>
        {
            var result = await sender.Send(new GetShoppingCartQuery(userId));

            var response = result.Adapt<GetShoppingCartResponse>();

            return Results.Ok(response);
        })
            .WithName("GetShoppingCart")
            .Produces<GetShoppingCartResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Shoppint Cart")
            .WithDescription("Get Shoppint Cart");

    }
}
