namespace Framework.Queries;

public class PaginatedItems<T>(IReadOnlyCollection<T> items, int totalCount, int pageIndex, int pageSize)
{
    public static PaginatedItems<T> Empty(int pageSize = int.MaxValue)
    {
        return new PaginatedItems<T>([], 0, 0, pageSize);
    }

    public IReadOnlyCollection<T> Items { get; } = items;
    public int TotalCount { get; } = totalCount;
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
}
