﻿using BlogPlatform.Accounts.Application.Common.Pagination;

namespace BlogPlatform.Accounts.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(
        this IQueryable<T> records, PaginationFilter filter)
    {
        if (filter is null)
        {
            return records;
        }

        return records.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);
    }
}
