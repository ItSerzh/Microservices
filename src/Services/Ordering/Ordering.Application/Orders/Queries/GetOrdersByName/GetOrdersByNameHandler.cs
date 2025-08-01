namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IOrderingDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.OrderName.Value.Contains(query.OrderName))
            .OrderBy(o => o.OrderName)
            .AsNoTracking()
            .ToListAsync();

        var orderDtos = orders.ToOrderDto();

        return new GetOrdersByNameResult(orderDtos);
    }
}

