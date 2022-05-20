using Comments.DataAccess.Filters;

namespace Comments.BusinessLogic.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> records, PaginationFilter filter)
    {
        if (filter is null)
            return records;

        return records.Skip((filter.PageNumber - 1) * filter.PageSize)
                      .Take(filter.PageSize);
    }
}
