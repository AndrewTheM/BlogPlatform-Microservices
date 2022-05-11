using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using BlogPlatform.Posts.BusinessLogic.DTO.Responses;
using BlogPlatform.Posts.BusinessLogic.Helpers;
using BlogPlatform.Posts.DataAccess.Extensions;
using BlogPlatform.Posts.DataAccess.Filters;

namespace BlogPlatform.Posts.BusinessLogic.Services.Contracts;

public interface IPostService
{
    /// <summary>
    /// Returns a page of posts determined by <paramref name="filter"/>.
    /// </summary>
    Task<Page<PostResponse>> GetPageOfPostsAsync(PostFilter filter = null);

    Task<IEnumerable<PostResponse>> GetTrendingPostsAsync(int count);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no post with given <paramref name="id"/> is found.
    /// </summary>
    Task<PostResponse> FindPostAsync(Guid id);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no post with given <paramref name="titleIdentifier"/> is found.
    /// </summary>
    Task<CompletePostResponse> GetCompletePostAsync(string titleIdentifier);

    Task<PostResponse> PublishPostAsync(PostRequest post);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no post with given <paramref name="id"/> is found.
    /// </summary>
    Task EditPostAsync(Guid id, PostRequest post);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no post with given <paramref name="id"/> is found.
    /// </summary>
    Task DeletePostAsync(Guid id);

    /// <summary>
    /// Adds a tag to post for each name in <paramref name="tagNames"/>.
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no post with given <paramref name="id"/> is found.
    /// </summary>
    Task SetTagsOfPostAsync(Guid id, params string[] tagNames);

    /// <summary>
    /// Throws <see cref="EntityNotFoundException"/>
    /// if no post with given <paramref name="id"/> is found.
    /// </summary>
    Task<bool> CheckIsPostAuthorAsync(Guid id, Guid userId);
}
