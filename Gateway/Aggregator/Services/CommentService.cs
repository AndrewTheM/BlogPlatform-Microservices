using Aggregator.DTO;
using Aggregator.Services.Contracts;

namespace Aggregator.Services;

public class CommentService : ICommentService
{
    private readonly HttpClient _client;

    public CommentService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Page<CommentDto>> GetPageOfPostCommentsAsync(Guid postId)
    {
        return await _client.GetFromJsonAsync<Page<CommentDto>>($"/api/comments/post/{postId}");
    }
}
