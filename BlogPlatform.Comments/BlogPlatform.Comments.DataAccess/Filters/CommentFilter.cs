namespace BlogPlatform.Comments.DataAccess.Filters;

public class CommentFilter : PaginationFilter
{
    public string Content { get; set; }

    public override CommentFilter CopyWithDifferentPage(int pageNumber)
    {
        return base.CopyWithDifferentPage(pageNumber) as CommentFilter;
    }
}
