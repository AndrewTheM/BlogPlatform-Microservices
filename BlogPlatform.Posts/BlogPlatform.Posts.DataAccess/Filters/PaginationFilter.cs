namespace BlogPlatform.Posts.DataAccess.Filters
{
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
            var copy = (PaginationFilter)MemberwiseClone();
            copy.PageNumber = pageNumber;
            return copy;
        }
    }
}
