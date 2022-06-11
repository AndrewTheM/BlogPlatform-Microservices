namespace BlogPlatform.UI.Models;

public class Page<T>
{
    public IEnumerable<T> Data { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }

    public string PreviousPage { get; set; }

    public string NextPage { get; set; }
}
