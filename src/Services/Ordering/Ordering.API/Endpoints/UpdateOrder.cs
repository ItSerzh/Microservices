using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();

            var responce = await mediator.Send(command, cancellationToken);

            var result = responce.Adapt<UpdateOrderResponse>();

            return Results.Ok(result);
        })
            .WithName("UpdateOrder")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDisplayName("Update Order")
            .WithDescription("Update Order");
    }
}
