using BlogPlatform.Shared.Web.Extensions;
using Posts.BusinessLogic.Services;
using Posts.BusinessLogic.Services.Contracts;
using Posts.DataAccess.Context;
using Posts.DataAccess.Context.Contracts;
using Posts.DataAccess.Repositories;
using Posts.DataAccess.Repositories.Contracts;

namespace Posts.API.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IPostRepository, PostRepository>();
        services.AddTransient<IPostContentRepository, PostContentRepository>();
        services.AddTransient<IRatingRepository, RatingRepository>();
        services.AddTransient<ITagRepository, TagRepository>();

        services.AddTransient<IBloggingUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddBlogging(this IServiceCollection services)
    {
        services.AddTransient<IPostService, PostService>();
        services.AddTransient<IRatingService, RatingService>();
        services.AddHelperServices();

        return services;
    }
}
