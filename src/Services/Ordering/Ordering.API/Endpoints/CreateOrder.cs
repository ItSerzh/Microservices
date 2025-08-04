using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);

public record CreateOrderRespose(Guid OrderId);

public class CreateOrder() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CreateOrderCommand>();

            var response = await mediator.Send(command, cancellationToken);

            var result = response.Adapt<CreateOrderRespose>();

            return Results.Created($"/orders/{response.OrderId}", result);
        })
            .WithName("CreateOrder")
            .Produces<CreateOrderRespose>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDisplayName("Create Order")
            .WithDescription("Create Order");
    }
}
