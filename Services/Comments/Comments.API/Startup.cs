using BlogPlatform.Shared.Web.Extensions;
using BlogPlatform.Shared.Web.Filters;
using Comments.BusinessLogic.Mapping;
using Comments.BusinessLogic.Services;
using Comments.BusinessLogic.Services.Contracts;
using Comments.DataAccess.Database;
using Comments.DataAccess.Database.Contracts;
using Comments.DataAccess.Factories;
using Comments.DataAccess.Factories.Contracts;
using Comments.DataAccess.Repositories;
using Comments.DataAccess.Repositories.Contracts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using System.Globalization;

namespace Comments.API;

internal class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddAutoMapper(typeof(CommentMappingProfile).Assembly);

        string connectionString = _configuration.GetConnectionString("LocalSqlServer");
        services.AddTransient<IConnectionFactory, SqlConnectionFactory>(
            _ => new SqlConnectionFactory(connectionString)
        );

        services.AddTransient<IGenerator, DatabaseGenerator>();

        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<ICommentService, CommentService>();
        services.AddHelperServices();

        services.AddLocalization(opt => opt.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(opts =>
        {
            var supportedCultures = new List<CultureInfo> { new("en"), new("uk"), new("ru") };
            opts.DefaultRequestCulture = new RequestCulture("en", "en");
            opts.SupportedCultures = supportedCultures;
            opts.SupportedUICultures = supportedCultures;
        });

        var versionsInfo = GetApiVersionsInfo();
        services.AddSwaggerWithSecurityAndVersioning(versionsInfo);

        services.AddControllers(options =>
        {
            options.Filters.Add<NotFoundExceptionFilterAttribute>();
        })
        .AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssemblyContaining<Startup>();
            config.DisableDataAnnotationsValidation = true;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
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

        app.UseRequestLocalization();
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
