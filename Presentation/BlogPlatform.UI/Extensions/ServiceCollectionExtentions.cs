using BlogPlatform.UI.Helpers;
using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Services;
using BlogPlatform.UI.Services.Contracts;

namespace BlogPlatform.UI.Extensions;

internal static class ServiceCollectionExtentions
{
    internal static IServiceCollection AddApiServices(this IServiceCollection services, string apiUrl)
    {
        void SetBaseApiRequestAddress(HttpClient client, string endpoint)
        {
            client.BaseAddress = new Uri($"{apiUrl}/{endpoint}");
        }

        services.AddTransient<IApiClient, ApiClient>();

        services.AddHttpClient<IPostService, PostService>(
            client => SetBaseApiRequestAddress(client, "posts")
        );

        services.AddHttpClient<ICommentService, CommentService>(
            client => SetBaseApiRequestAddress(client, "comments")
        );

        services.AddHttpClient<IRatingService, RatingService>(
            client => SetBaseApiRequestAddress(client, "posts/ratings")
        );

        services.AddHttpClient<ITagService, TagService>(
            client => SetBaseApiRequestAddress(client, "posts/tags")
        );

        services.AddHttpClient<IAuthService, AuthService>(
            client => SetBaseApiRequestAddress(client, "users")
        );

        services.AddHttpClient<IFileService, FileService>(
            client => SetBaseApiRequestAddress(client, "files")
        );

        return services;
    }
}
