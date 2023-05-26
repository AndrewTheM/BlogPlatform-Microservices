using BlogPlatform.Shared.Common.Filters;
using Microsoft.EntityFrameworkCore;
using Posts.DataAccess.Context;
using Posts.DataAccess.Entities;
using Posts.DataAccess.Repositories.Contracts;

namespace Posts.DataAccess.Repositories;

public class PostRepository : EntityRepository<Post>, IPostRepository
{
    public PostRepository(BlogContext context)
        : base(context)
    {
    }

    public Task<IQueryable<Post>> GetNewestPostsWithAuthorsAndTagsAsync()
    {
        var posts = _set.Include(p => p.Tags)
            .OrderByDescending(p => p.CreatedOn)
            .AsQueryable();

        return Task.FromResult(posts);
    }

    public async Task<IQueryable<Post>> GetFilteredPostsAsync(PostFilter filter)
    {
        var posts = await GetNewestPostsWithAuthorsAndTagsAsync();

        if (filter is null)
            return posts;

        return posts.Where(p => filter.Title == null || p.Title.Contains(filter.Title))
            .Where(p => filter.Author == null || p.Author == filter.Author)
            .Where(p => filter.Year == null || p.CreatedOn.Year == filter.Year.Value)
            .Where(p => filter.Month == null || p.CreatedOn.Month == filter.Month.Value)
            .Where(p => filter.Day == null || p.CreatedOn.Day == filter.Day.Value)
            .Where(p => filter.Tag == null || p.Tags.Any(t => t.TagName == filter.Tag));
    }

    public Task<IQueryable<Post>> GetTopRatedPostsWithAuthorsAsync(int count)
    {
        var posts = _set.Include(p => p.Ratings)
            .OrderByDescending(p => p.Ratings.Average(r => r.RatingValue))
            .Take(count);

        return Task.FromResult(posts);
    }

    public Task<Post> GetPostWithContentAsync(Guid id)
    {
        return EnsureEntityResultAsync(() =>
        {
            return _set.Include(p => p.ContentEntity)
                .SingleAsync(p => p.Id == id);
        });
    }

    public Task<Post> GetPostWithTagsAsync(Guid id)
    {
        return EnsureEntityResultAsync(() =>
        {
            return _set.Include(p => p.Tags)
                .SingleAsync(p => p.Id == id);
        });
    }

    public Task<Post> GetCompletePostAsync(string titleIdentifier)
    {
        return EnsureEntityResultAsync(() =>
        {
            return _set.Include(p => p.ContentEntity)
                .Include(p => p.Tags)
                .SingleOrDefaultAsync(p => p.TitleIdentifier == titleIdentifier);
        });
    }

    public async Task<double> CalculatePostRatingAsync(Guid id)
    {
        try
        {
            var post = await GetByIdAsync(id);

            return await _context.Entry(post)
                .Collection(p => p.Ratings)
                .Query()
                .AverageAsync(r => r.RatingValue);
        }
        catch (InvalidOperationException)
        {
            return 0;
        }
    }
}
