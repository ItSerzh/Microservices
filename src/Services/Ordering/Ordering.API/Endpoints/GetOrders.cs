using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

//public record GetOrdersRequest(PaginationRequest Request);

public record GetOrdersResponse(PaginationResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async([AsParameters] PaginationRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var responce = await sender.Send(new GetOrdersQuery(request), cancellationToken);

            var result = responce.Adapt<GetOrdersResponse>();

            return Results.Ok(result);
        })
            .WithName("GetOrders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDisplayName("Get Orders")
            .WithDescription("Get Orders");
    }
}
