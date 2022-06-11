using BlogPlatform.UI.Helpers;
using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Models;
using BlogPlatform.UI.Services.Contracts;

namespace BlogPlatform.UI.Services;

public class CommentService : ICommentService
{
    private readonly IApiClient _apiClient;

    public CommentService(IApiClient apiClient, HttpClient httpClient)
    {
        _apiClient = apiClient;
        _apiClient.HttpClient = httpClient;
    }

    public async Task<Page<Comment>> GetCommentPageForPostAsync(
        int pageNumber = 1, int pageSize = 10, string content = null)
    {
        UriQueryBuilder queryBuilder = new();
        Dictionary<string, string> parameters = new()
        {
            [nameof(pageNumber)] = pageNumber.ToString(),
            [nameof(pageSize)] = pageSize.ToString(),
            [nameof(content)] = content
        };

        queryBuilder.AppendParameters(parameters);
        string query = queryBuilder.ToString();
        return await GetCommentPageForPostAsync(query);
    }

    public async Task<Page<Comment>> GetCommentPageForPostAsync(string pageUrl)
    {
        return await _apiClient.SendGetApiRequestAsync<Page<Comment>>(pageUrl);
    }

    public async Task<Comment> PublishCommentAsync(Comment comment)
    {
        return await _apiClient.SendPostApiRequestWithResultAsync<Comment, Comment>(
            endpoint: "",
            body: comment
        );
    }

    public async Task EditCommentAsync(Guid id, Comment editedComment)
    {
        await _apiClient.SendModifyingApiRequestAsync(
            method: HttpMethod.Put,
            endpoint: $"comments/{id}",
            body: editedComment
        );
    }

    public async Task DeleteCommentAsync(Guid id)
    {
        await _apiClient.SendDeleteRequestAsync($"comments/{id}");
    }

    public async Task AddVoteToCommentAsync(Guid id, int voteValue)
    {
        await _apiClient.SendModifyingApiRequestAsync(
            method: HttpMethod.Patch,
            endpoint: $"comments/{id}",
            body: new { voteValue }
        );
    }
}
