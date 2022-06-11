using BlogPlatform.UI.Models;

namespace BlogPlatform.UI.Services.Contracts;

public interface IPostService
{
    Task<Page<Post>> GetPostsAsync(
        int pageNumber = 1, int pageSize = 10,
        string title = null, string author = null, string tag = null);

    Task<Page<Post>> GetPostsAsync(string pageUrl);

    Task<IEnumerable<Post>> GetTrendingPostsAsync();

    Task<Post> FindPostAsync(string titleIdentifier);

    Task<Post> PublishPostAsync(Post post);

    Task EditPostAsync(Post post);

    Task DeletePostAsync(Guid id);

    Task SetTagsOfPostAsync(Guid id, ISet<string> tags);
}
