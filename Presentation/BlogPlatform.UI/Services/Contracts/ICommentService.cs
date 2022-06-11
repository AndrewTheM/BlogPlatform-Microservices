using BlogPlatform.UI.Models;

namespace BlogPlatform.UI.Services.Contracts;

public interface ICommentService
{
    Task<Page<Comment>> GetCommentPageForPostAsync(
        int pageNumber = 1, int pageSize = 10, string contentQuery = null);

    Task<Page<Comment>> GetCommentPageForPostAsync(string pageUrl);

    Task<Comment> PublishCommentAsync(Comment comment);

    Task EditCommentAsync(Guid id, Comment editedComment);

    Task DeleteCommentAsync(Guid id);

    Task AddVoteToCommentAsync(Guid id, int voteValue);
}
