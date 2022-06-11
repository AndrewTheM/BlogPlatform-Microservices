using BlogPlatform.Shared.Common.Filters;

namespace BlogPlatform.Shared.Common.Pagination;

public class Page<T>
{
    private IEnumerable<T> _data;

    public IEnumerable<T> Data
    {
        get => _data;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _data = value;
        }
    }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }

    public string PreviousPage { get; set; }

    public string NextPage { get; set; }

    public Page()
    {
    }

    public Page(
        IEnumerable<T> data,
        int totalRecords,
        PaginationFilter filter = null,
        Uri previousPageUri = null,
        Uri nextPageUri = null)
    {
        Data = data;
        TotalRecords = totalRecords;
        PageNumber = filter?.PageNumber ?? 0;
        PageSize = filter?.PageSize ?? 0;

        if (PageSize > 0)
        {
            TotalPages = (int)Math.Ceiling(TotalRecords * 1d / PageSize);
        }

        if (PageNumber > 1 && Data.Any())
        {
            PreviousPage = previousPageUri?.AbsoluteUri;
        }

        if (PageNumber < TotalPages)
        {
            NextPage = nextPageUri?.AbsoluteUri;
        }
    }
}
