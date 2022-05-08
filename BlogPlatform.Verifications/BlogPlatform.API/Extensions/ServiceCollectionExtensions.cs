using BlogPlatform.DataAccess.Context;
using BlogPlatform.DataAccess.Context.Contracts;
using BlogPlatform.DataAccess.Repositories;
using BlogPlatform.DataAccess.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace BlogPlatform.API.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IVerificationRepository, VerificationRepository>();
            services.AddTransient<IVerificationStatusRepository, VerificationStatusRepository>();

            services.AddTransient<IBloggingUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddBlogging(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddSwaggerWithSecurityAndVersioning(this IServiceCollection services,
                                                                             IEnumerable<OpenApiInfo> versionsInfo)
        {
            return services.AddSwaggerGen(c =>
            {
                foreach (var apiInfo in versionsInfo)
                    c.SwaggerDoc(apiInfo.Version, apiInfo);

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
                {
                    {
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
                    }
                });
            });
        }
    }
}
