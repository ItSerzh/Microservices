
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryHandler(IDocumentSession documentSession, ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Called query {query}");

        var products = await documentSession.Query<Product>()
            .Where(p => p.Catageories.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
