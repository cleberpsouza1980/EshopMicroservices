namespace BuildingBlocks.Pagination;

public class PaginationResult <TEntity>
    (int pageIndex, int pageSize, long totalCount, IReadOnlyList<TEntity> data) 
    where TEntity : notnull
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long TotalCount { get; } = totalCount;
    public IReadOnlyList<TEntity> Data { get; } = data;
}
