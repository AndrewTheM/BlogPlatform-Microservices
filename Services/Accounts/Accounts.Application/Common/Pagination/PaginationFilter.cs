namespace Accounts.Application.Common.Pagination;

public class PaginationFilter
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public PaginationFilter()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public PaginationFilter CopyWithDifferentPage(int pageNumber)
    {
        var copy = MemberwiseClone() as PaginationFilter;
        copy.PageNumber = pageNumber;
        return copy;
    }
}
