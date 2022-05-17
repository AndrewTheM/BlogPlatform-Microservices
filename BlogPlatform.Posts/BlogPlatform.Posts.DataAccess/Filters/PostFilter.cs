namespace BlogPlatform.Posts.DataAccess.Filters;

public class PostFilter : PaginationFilter
{
    public string Title { get; set; }

    public string Author { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public int? Day { get; set; }

    public string Tag { get; set; }

    public override PostFilter CopyWithDifferentPage(int pageNumber)
    {
        return base.CopyWithDifferentPage(pageNumber) as PostFilter;
    }

    public override bool Equals(object obj)
    {
        return obj is PostFilter filter &&
               PageNumber == filter.PageNumber &&
               PageSize == filter.PageSize &&
               Title == filter.Title &&
               Author == filter.Author &&
               Year == filter.Year &&
               Month == filter.Month &&
               Day == filter.Day &&
               Tag == filter.Tag;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            PageNumber, PageSize, Title,
            Author, Year, Month, Day, Tag);
    }
}
