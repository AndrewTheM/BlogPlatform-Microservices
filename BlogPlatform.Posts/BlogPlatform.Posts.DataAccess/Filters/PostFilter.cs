namespace BlogPlatform.Posts.DataAccess.Filters
{
    public class PostFilter : PaginationFilter
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int? Year { get; set; }

        public int? Month { get; set; }

        public int? Day { get; set; }

        public string Tag { get; set; }

        public new PostFilter CopyWithDifferentPage(int pageNumber)
            => (PostFilter)base.CopyWithDifferentPage(pageNumber);
    }
}
