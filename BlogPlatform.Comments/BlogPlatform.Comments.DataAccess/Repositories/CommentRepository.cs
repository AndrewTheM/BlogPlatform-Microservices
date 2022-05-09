using BlogPlatform.Comments.DataAccess.Entities;
using BlogPlatform.Comments.DataAccess.Filters;
using BlogPlatform.Comments.DataAccess.Repositories.Contracts;

namespace BlogPlatform.Comments.DataAccess.Repositories;

public class CommentRepository : ICommentRepository
{
    public async Task<IQueryable<Comment>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Comment> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateAsync(Comment entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Guid id, Comment newEntity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    // TODO: work with other microservices
    public async Task<IQueryable<Comment>> GetCommentsOfPostAsync(Guid postId)
    {
        throw new NotImplementedException();

        //var comments = result.Where(c => c.PostId == postId)
        //                     .OrderByDescending(c => c.CreatedOn);
        //return await Task.FromResult(comments);
    }

    public async Task<IQueryable<Comment>> GetFilteredCommentsOfPostAsync(Guid postId, CommentFilter filter)
    {
        var comments = await this.GetCommentsOfPostAsync(postId);

        if (filter is null)
            return comments;

        return comments.Where(c => filter.Content == null || c.Content.Contains(filter.Content));
    }

    // TODO: work with other microservices
    public async Task<Comment> GetCommentWithAuthorAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
