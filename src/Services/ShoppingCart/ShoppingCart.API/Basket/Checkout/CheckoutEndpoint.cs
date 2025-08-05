namespace ShoppingCart.API.Basket.Checkout;

public record CheckoutRequest(CheckoutDto CheckoutDto);

public record CheckoutResponse(bool IsSuccess);

public class CheckoutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/shopping-cart/chekcout", async (CheckoutRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CheckoutCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CheckoutResponse>();

            return Results.Ok(response);
        })
            .WithName("ShoppingCartCheckout")
            .Produces<CheckoutResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDisplayName("Shopping Cart Checkout")
            .WithDescription("Shopping Cart Checkout");
    }
}
