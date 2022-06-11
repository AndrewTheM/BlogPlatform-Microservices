using BlogPlatform.UI.Models;

namespace BlogPlatform.UI.Services.Contracts;

public interface IRatingService
{
    Task<Rating> GetRatingForPostAsync(Guid postId, Guid userId);

    Task<Rating> RatePostAsync(Rating rating);

    Task ChangeRatingAsync(Guid id, Rating newRating);

    Task ClearRatingAsync(Guid id);
}
