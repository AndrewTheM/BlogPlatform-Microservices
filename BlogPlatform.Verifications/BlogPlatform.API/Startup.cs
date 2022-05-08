using BlogPlatform.API.Extensions;
using BlogPlatform.DataAccess.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Globalization;

namespace BlogPlatform.API
{
    public class Startup
    {
        private class ConfigurationNames
        {
            public const string DbConnection = "LocalSqlServer";
        }

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IEnumerable<OpenApiInfo> GetApiVersionsInfo()
        {
            var versionsConfig = _configuration.GetSection("Versions");
            var apiInfos = versionsConfig.Get<OpenApiInfo[]>();
            return apiInfos;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDbContext<BlogContext>(options =>
            {
                string configName = ConfigurationNames.DbConnection;
                string connectionString = _configuration.GetConnectionString(configName);
                options.UseSqlServer(connectionString);
            });

            services.AddHttpContextAccessor();

            services.AddRepositories();
            services.AddBlogging();

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
                        config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
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
    }
}
