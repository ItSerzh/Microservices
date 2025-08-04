using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

//public record DeleteOrderRequest(Guid Id);

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new DeleteOrderCommand(id), cancellationToken);

            var result = response.Adapt<DeleteOrderResponse>();

            return Results.Ok(result);
        })
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDisplayName("Delete Order")
            .WithDescription("Delete Order");
    }
}
