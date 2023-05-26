using BlogPlatform.Shared.Common.Exceptions;
using BlogPlatform.Shared.Common.Filters;
using BlogPlatform.Shared.Common.Pagination;
using Comments.BusinessLogic.DTO.Requests;
using Comments.BusinessLogic.DTO.Responses;

namespace Comments.BusinessLogic.Services.Contracts;

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

    Task<CommentResponse> PublishCommentAsync(CommentRequest commentDto, Guid userId, string username);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no comment with given <paramref name="id"/> is found.
    /// </summary>
    Task EditCommentAsync(Guid id, CommentContentRequest commentDto);

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
    Task<bool> CheckIsCommentAuthorAsync(Guid id, Guid userId);
}
