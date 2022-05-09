using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using BlogPlatform.Comments.BusinessLogic.DTO.Responses;
using BlogPlatform.Comments.DataAccess.Extensions;
using BlogPlatform.Comments.DataAccess.Filters;
using BlogPlatform.Comments.BusinessLogic.Helpers;

namespace BlogPlatform.Comments.BusinessLogic.Services.Contracts;

public interface ICommentService
{
    /// <summary>
    /// Returns a page of post comments
    /// determined by <paramref name="filter"/>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no post with given <paramref name="postId"/> is found.
    /// </summary>
    Task<Page<CommentResponse>> GetPageOfCommentsForPostAsync(Guid postId, CommentFilter filter = null);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no comment with given <paramref name="id"/> is found.
    /// </summary>
    Task<CommentResponse> GetCommentByIdAsync(Guid id);

    Task<CommentResponse> PublishCommentAsync(CommentRequest commentDto, string authorId);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no comment with given <paramref name="id"/> is found.
    /// </summary>
    Task EditCommentAsync(Guid id, CommentRequest commentDto);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no comment with given <paramref name="id"/> is found.
    /// </summary>
    Task DeleteCommentAsync(Guid id);

    /// <summary>
    /// Increases or decreases the upvotes of comment,
    /// based on <paramref name="voteValue"/>.
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no comment with given <paramref name="id"/> is found.
    /// Throws <see cref="ArgumentOutOfRangeException"/>
    /// if <paramref name="voteValue"/> is invalid.
    /// </summary>
    Task AddVoteToCommentAsync(Guid id, int voteValue);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no post with given <paramref name="id"/> is found.
    /// </summary>
    Task<bool> CheckIsCommentAuthorAsync(Guid id, string userId);
}
