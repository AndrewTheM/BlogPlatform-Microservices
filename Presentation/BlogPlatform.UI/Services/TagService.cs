using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Services.Contracts;

namespace BlogPlatform.UI.Services;

public class TagService : ITagService
{
    private readonly IApiClient _apiClient;

    public TagService(IApiClient apiClient, HttpClient httpClient)
    {
        _apiClient = apiClient;
        _apiClient.HttpClient = httpClient;
    }

    public async Task<List<string>> GetTagNames()
    {
        return await _apiClient.SendGetApiRequestAsync<List<string>>("");
    }
}
