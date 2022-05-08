namespace BlogPlatform.Comments.DataAccess.Filters
{
    public class CommentFilter : PaginationFilter
    {
        public string Content { get; set; }

        public new CommentFilter CopyWithDifferentPage(int pageNumber)
            => (CommentFilter)base.CopyWithDifferentPage(pageNumber);
    }
}
