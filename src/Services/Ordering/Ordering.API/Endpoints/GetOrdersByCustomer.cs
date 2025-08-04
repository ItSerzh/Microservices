using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

//public record GetOrderByCustomerReques(string OrderName);

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetOrdersByCustomerQuery(customerId), cancellationToken);

            var result = response.Adapt<GetOrdersByCustomerResponse>();

            return Results.Ok(result);
        })
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDisplayName("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
    }
}
