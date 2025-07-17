namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession documentSession,
                                          ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Called with query {query}");
        
        var product = await documentSession.LoadAsync<Product>(query.Id, cancellationToken);

        return product is null ? throw new ProductNotFoundException() : new GetProductByIdResult(product);
    }
}
