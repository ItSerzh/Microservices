using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

//public record GetOrderByNameReques(string OrderName);

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetOrdersByNameQuery(orderName), cancellationToken);

            var result = response.Adapt<GetOrdersByNameResponse>();

            return Results.Ok(result);
        })
            .WithName("GetOrdersByName")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDisplayName("Get Orders By Name")
            .WithDescription("Get Orders By Name");
    }
}
