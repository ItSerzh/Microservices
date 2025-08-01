namespace BuildingBlocks.Pagination;

public class PaginationResult<TEntity>(int pageIndex, int pageSize, long totalPages, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; } = pageIndex;

    public int PageSize { get; } = pageSize;

    public long TotalItems { get; } = totalPages;

    public IEnumerable<TEntity> Data { get; } = data;
}

