using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Models;
using BlogPlatform.UI.Services.Contracts;

namespace BlogPlatform.UI.Services;

public class RatingService : IRatingService
{
    private readonly IApiClient _apiClient;

    public RatingService(IApiClient apiClient, HttpClient httpClient)
    {
        _apiClient = apiClient;
        _apiClient.HttpClient = httpClient;
    }

    public async Task<Rating> GetRatingForPostAsync(Guid postId, Guid userId)
    {
        return await _apiClient.SendGetApiRequestAsync<Rating>($"ratings/{postId}/{userId}");
    }

    public async Task<Rating> RatePostAsync(Rating rating)
    {
        return await _apiClient.SendPostApiRequestWithResultAsync<Rating, Rating>(
            endpoint: "",
            body: rating
        );
    }

    public async Task ChangeRatingAsync(Guid id, Rating newRating)
    {
        await _apiClient.SendModifyingApiRequestAsync(
            method: HttpMethod.Put,
            endpoint: $"ratings/{id}",
            body: newRating
        );
    }

    public async Task ClearRatingAsync(Guid id)
    {
        await _apiClient.SendDeleteRequestAsync($"ratings/{id}");
    }
}
