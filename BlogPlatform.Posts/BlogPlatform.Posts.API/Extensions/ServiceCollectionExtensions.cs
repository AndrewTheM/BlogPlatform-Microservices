using BlogPlatform.Posts.BusinessLogic.Services;
using BlogPlatform.Posts.BusinessLogic.Services.Contracts;
using BlogPlatform.Posts.DataAccess.Context;
using BlogPlatform.Posts.DataAccess.Context.Contracts;
using BlogPlatform.Posts.DataAccess.Repositories;
using BlogPlatform.Posts.DataAccess.Repositories.Contracts;
using Microsoft.OpenApi.Models;

namespace BlogPlatform.Posts.API.Extensions;

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
        services.AddTransient<ITimeService, TimeService>();

        services.AddTransient<IUriService>(provider =>
        {
            var contextAccesor = provider.GetService<IHttpContextAccessor>();
            var request = contextAccesor.HttpContext.Request;
            string uri = $"{request.Scheme}://{request.Host}";
            return new UriService(uri);
        });

        return services;
    }

    public static IServiceCollection AddSwaggerWithSecurityAndVersioning(
        this IServiceCollection services, IEnumerable<OpenApiInfo> versionsInfo)
    {
        return services.AddSwaggerGen(c =>
        {
            foreach (var apiInfo in versionsInfo)
            {
                c.SwaggerDoc(apiInfo.Version, apiInfo);
            }

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme.
                                Enter 'Bearer' [space] and then your token in the
                                text input below. Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }});
        });
    }
}
