using BlogPlatform.UI.Helpers;
using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Models;
using BlogPlatform.UI.Services.Contracts;

namespace BlogPlatform.UI.Services;

public class PostService : IPostService
{
    private readonly IApiClient _apiClient;

    public PostService(IApiClient apiClient, HttpClient httpClient)
    {
        _apiClient = apiClient;
        _apiClient.HttpClient = httpClient;
    }

    public Task<Page<Post>> GetPostsAsync(
        int pageNumber = 1, int pageSize = 10,
        string title = null, string author = null, string tag = null)
    {
        var queryBuilder = new UriQueryBuilder();
        var parameters = new Dictionary<string, string>
        {
            [nameof(pageNumber)] = pageNumber.ToString(),
            [nameof(pageSize)] = pageSize.ToString(),
            [nameof(title)] = title,
            [nameof(author)] = author,
            [nameof(tag)] = tag
        };

        queryBuilder.AppendParameters(parameters);
        var query = queryBuilder.ToString();
        return GetPostsAsync(query);
    }

    public Task<Page<Post>> GetPostsAsync(string pageUrl)
    {
        return _apiClient.SendGetApiRequestAsync<Page<Post>>(pageUrl);
    }

    public Task<List<Post>> GetTrendingPostsAsync()
    {
        return _apiClient.SendGetApiRequestAsync<List<Post>>("posts/trending?top=5");
    }

    public Task<Post> FindPostAsync(string titleIdentifier)
    {
        return _apiClient.SendGetApiRequestAsync<Post>($"posts/complete/{titleIdentifier}");
    }

    public Task<Post> PublishPostAsync(Post post)
    {
        return _apiClient.SendPostApiRequestWithResultAsync<Post, Post>(
            endpoint: "",
            body: post
        );
    }

    public Task EditPostAsync(Post post)
    {
        return _apiClient.SendModifyingApiRequestAsync(
            method: HttpMethod.Put,
            endpoint: $"posts/{post.Id}",
            body: post
        );
    }

    public Task SetTagsOfPostAsync(Guid id, ISet<string> tags)
    {
        return _apiClient.SendModifyingApiRequestAsync(
            method: HttpMethod.Post,
            endpoint: $"posts/{id}/tags",
            body: new { tags }
        );
    }

    public Task DeletePostAsync(Guid id)
    {
        return _apiClient.SendDeleteRequestAsync($"posts/{id}");
    }
}
