using Accounts.Application.Common.Contracts;
using Accounts.Application.Common.Mapping;
using Accounts.Application.Features;
using Accounts.Infrastructure.Persistence;
using BlogPlatform.Shared.Web.Extensions;
using BlogPlatform.Shared.Web.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Accounts.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, AppDbContext>(options =>
        {
            string connectionString = _configuration.GetConnectionString("LocalSqlServer");
            options.UseSqlServer(connectionString);
        });

        services.AddMediatR(typeof(MediatrDI).Assembly);
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        var versionsInfo = GetApiVersionsInfo();
        services.AddSwaggerWithSecurityAndVersioning(versionsInfo);

        services.AddControllers(options =>
        {
            options.Filters.Add<NotFoundExceptionFilterAttribute>();
        })
        .AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssemblyContaining<MediatrDI>();
            config.DisableDataAnnotationsValidation = true;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var apiInfo in GetApiVersionsInfo())
                {
                    c.SwaggerEndpoint($"/swagger/{apiInfo.Version}/swagger.json",
                                      $"{apiInfo.Title} {apiInfo.Version}");
                }
            });
        }

        app.UseRouting();

        // TODO: configure CORS properly
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private IEnumerable<OpenApiInfo> GetApiVersionsInfo()
    {
        var versionsConfig = _configuration.GetSection("Versions");
        var apiInfos = versionsConfig.Get<OpenApiInfo[]>();
        return apiInfos;
    }
}
