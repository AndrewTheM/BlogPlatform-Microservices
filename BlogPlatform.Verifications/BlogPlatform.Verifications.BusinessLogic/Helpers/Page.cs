using BlogPlatform.Verifications.DataAccess.Filters;

namespace BlogPlatform.Verifications.BusinessLogic.Helpers;

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

    public int TotalPages
    {
        get
        {
            if (PageSize == 0)
            {
                return 0;
            }

            return (int)Math.Ceiling(TotalRecords * 1d / PageSize);
        }
    }

    public string PreviousPage { get; set; }

    public string NextPage { get; set; }

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
