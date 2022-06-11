using BlogPlatform.UI.Helpers;
using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Models;
using BlogPlatform.UI.Services.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogPlatform.UI.Services;

public class PostService : IPostService
{
    private readonly IApiClient _apiClient;

    public PostService(IApiClient apiClient, HttpClient httpClient)
    {
        _apiClient = apiClient;
        _apiClient.HttpClient = httpClient;
    }

    public async Task<Page<Post>> GetPostsAsync(
        int pageNumber = 1, int pageSize = 10,
        string title = null, string author = null, string tag = null)
    {
        UriQueryBuilder queryBuilder = new();
        Dictionary<string, string> parameters = new()
        {
            [nameof(pageNumber)] = pageNumber.ToString(),
            [nameof(pageSize)] = pageSize.ToString(),
            [nameof(title)] = title,
            [nameof(author)] = author,
            [nameof(tag)] = tag
        };

        queryBuilder.AppendParameters(parameters);
        string query = queryBuilder.ToString();
        return await GetPostsAsync(query);
    }

    public async Task<Page<Post>> GetPostsAsync(string pageUrl)
    {
        return await _apiClient.SendGetApiRequestAsync<Page<Post>>(pageUrl);
    }

    public async Task<IEnumerable<Post>> GetTrendingPostsAsync()
    {
        return await _apiClient.SendGetApiRequestAsync<IEnumerable<Post>>("posts/trending?top=5");
    }

    public async Task<Post> FindPostAsync(string titleIdentifier)
    {
        return await _apiClient.SendGetApiRequestAsync<Post>($"posts/complete/{titleIdentifier}");
    }

    public async Task<Post> PublishPostAsync(Post post)
    {
        return await _apiClient.SendPostApiRequestWithResultAsync<Post, Post>(
            endpoint: "",
            body: post
        );
    }

    public async Task EditPostAsync(Post post)
    {
        await _apiClient.SendModifyingApiRequestAsync(
            method: HttpMethod.Put,
            endpoint: $"posts/{post.Id}",
            body: post
        );
    }

    public async Task SetTagsOfPostAsync(Guid id, ISet<string> tags)
    {
        await _apiClient.SendModifyingApiRequestAsync(
            method: HttpMethod.Post,
            endpoint: $"posts/{id}/tags",
            body: new { tags }
        );
    }

    public async Task DeletePostAsync(Guid id)
    {
        await _apiClient.SendDeleteRequestAsync($"posts/{id}");
    }
}
