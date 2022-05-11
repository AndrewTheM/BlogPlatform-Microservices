﻿using BlogPlatform.Verifications.DataAccess.Context;
using BlogPlatform.Verifications.DataAccess.Context.Contracts;
using BlogPlatform.Verifications.DataAccess.Repositories;
using BlogPlatform.Verifications.DataAccess.Repositories.Contracts;
using Microsoft.OpenApi.Models;

namespace BlogPlatform.Verifications.API.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IVerificationRepository, VerificationRepository>();
        services.AddTransient<IVerificationStatusRepository, VerificationStatusRepository>();

        services.AddTransient<IBloggingUnitOfWork, UnitOfWork>();

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
                    In = ParameterLocation.Header
                },
                new List<string>()
            }});
        });
    }
}
