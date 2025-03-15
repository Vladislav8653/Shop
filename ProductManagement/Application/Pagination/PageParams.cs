namespace ProductManagement.Application.Pagination;

public class PageParams(int page = 1, int pageSize = 10)
{
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}