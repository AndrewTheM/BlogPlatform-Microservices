using BlogPlatform.Shared.Services;
using BlogPlatform.Shared.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BlogPlatform.Shared.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHelperServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

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
