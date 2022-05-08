using BlogPlatform.Posts.DataAccess.Context;
using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Filters;
using BlogPlatform.Posts.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Posts.DataAccess.Repositories
{
    public class PostRepository : EntityRepository<Post, int>, IPostRepository
    {
        public PostRepository(BlogContext context)
            : base(context)
        {
        }

        // TODO: work with other microservices
        public async Task<IQueryable<Post>> GetNewestPostsWithAuthorsAndTagsAsync()
        {
            var posts = _set//.Include(p => p.Author)
                            .Include(p => p.Tags)
                            .OrderByDescending(p => p.CreatedOn);
            return await Task.FromResult(posts);
        }

        // TODO: work with other microservices
        public async Task<IQueryable<Post>> GetFilteredPostsAsync(PostFilter filter)
        {
            var posts = await GetNewestPostsWithAuthorsAndTagsAsync();

            if (filter is null)
                return posts;

            return posts.Where(p => filter.Title == null || p.Title.Contains(filter.Title))
                        //.Where(p => filter.Author == null || p.Author.UserName == filter.Author)
                        .Where(p => filter.Year == null || p.CreatedOn.Year == filter.Year.Value)
                        .Where(p => filter.Month == null || p.CreatedOn.Month == filter.Month.Value)
                        .Where(p => filter.Day == null || p.CreatedOn.Day == filter.Day.Value)
                        .Where(p => filter.Tag == null || p.Tags.Any(t => t.TagName == filter.Tag));
        }

        // TODO: work with other microservices
        public async Task<IQueryable<Post>> GetTopRatedPostsWithAuthorsAsync(int count)
        {
            var posts = _set//.Include(p => p.Author)
                            .Include(p => p.Ratings)
                            .OrderByDescending(p => p.Ratings.Average(r => r.RatingValue))
                            .Take(count);
            return await Task.FromResult(posts);
        }

        public async Task<Post> GetPostWithContentAsync(int id)
        {
            return await EnsureEntityResultAsync(() =>
            {
                return _set.Include(p => p.ContentEntity)
                           .SingleAsync(p => p.Id == id);
            });
        }

        public async Task<Post> GetPostWithTagsAsync(int id)
        {
            return await EnsureEntityResultAsync(() =>
            {
                return _set.Include(p => p.Tags)
                           .SingleAsync(p => p.Id == id);
            });
        }

        // TODO: work with other microservices
        public async Task<Post> GetCompletePostAsync(string titleIdentifier)
        {
            return await EnsureEntityResultAsync(() =>
            {
                return _set.Include(p => p.ContentEntity)
                           //.Include(p => p.Author)
                           .Include(p => p.Tags)
                           .SingleAsync(p => p.TitleIdentifier == titleIdentifier);
            });
        }

        public async Task<double> CalculatePostRatingAsync(int id)
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
}
