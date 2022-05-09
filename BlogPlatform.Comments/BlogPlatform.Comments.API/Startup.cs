using BlogPlatform.Comments.API.Extensions;
using BlogPlatform.Comments.BusinessLogic.Mapping;
using BlogPlatform.Comments.BusinessLogic.Services;
using BlogPlatform.Comments.BusinessLogic.Services.Contracts;
using BlogPlatform.Comments.DataAccess.Repositories;
using BlogPlatform.Comments.DataAccess.Repositories.Contracts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using System.Globalization;

namespace BlogPlatform.Comments.API;

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
        services.AddHttpContextAccessor();

        services.AddAutoMapper(typeof(CommentMappingProfile).Assembly);

        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<ICommentService, CommentService>();
        services.AddTransient<ITimeService, TimeService>();

        services.AddTransient<IUriService>(provider =>
        {
            var contextAccesor = provider.GetService<IHttpContextAccessor>();
            var request = contextAccesor.HttpContext.Request;
            string uri = $"{request.Scheme}://{request.Host}";
            return new UriService(uri);
        });

        services.AddLocalization(opt => opt.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(opts =>
        {
            var supportedCultures = new List<CultureInfo> { new("en"), new("uk"), new("ru") };
            opts.DefaultRequestCulture = new RequestCulture("en", "en");
            opts.SupportedCultures = supportedCultures;
            opts.SupportedUICultures = supportedCultures;
        });

        var versionsInfo = this.GetApiVersionsInfo();
        services.AddSwaggerWithSecurityAndVersioning(versionsInfo);

        services.AddControllers()
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
                foreach (var apiInfo in this.GetApiVersionsInfo())
                {
                    c.SwaggerEndpoint($"/swagger/{apiInfo.Version}/swagger.json",
                                      $"{apiInfo.Title} {apiInfo.Version}");
                }
            });
        }

        app.UseHttpsRedirection();
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
