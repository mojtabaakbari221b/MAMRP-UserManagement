namespace Share.Helper;

public class PaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    private int MaxPageSize { get; set; } = 1000;
    private int DefaultPageSize { get; set; } = 10;
    public PaginationFilter()
    {
        this.PageNumber = 1;
        this.PageSize = DefaultPageSize;
    }
    public PaginationFilter(int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        this.PageSize = pageSize > MaxPageSize ? DefaultPageSize : pageSize;
    }
}