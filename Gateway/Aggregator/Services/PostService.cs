using Aggregator.DTO;
using Aggregator.Services.Contracts;

namespace Aggregator.Services;

public class PostService : IPostService
{
    private readonly HttpClient _client;

    public PostService(HttpClient client)
    {
        _client = client;
    }

    public async Task<CompletePostDto> GetCompletePostAsync(string titleIdentifier)
    {
        return await _client.GetFromJsonAsync<CompletePostDto>($"/api/posts/complete/{titleIdentifier}");
    }
}
