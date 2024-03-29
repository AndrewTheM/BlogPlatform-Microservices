﻿using IdentityServer.Data;
using IdentityServer.Data.Entities;
using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string assemblyName = GetType().Assembly.GetName().Name;
        string connectionString = Configuration.GetConnectionString("LocalSqlServer");

        services.AddDbContext<ApplicationDbContext>(
            opts => opts.UseSqlServer(connectionString));

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer(options =>
        {
            options.IssuerUri = Configuration["IssuerUrl"];
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
        })
        .AddAspNetIdentity<ApplicationUser>()
        .AddProfileService<ProfileService>()
        .AddDeveloperSigningCredential()
        .AddConfigurationStore(config =>
        {
            config.ConfigureDbContext = builder =>
                builder.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(assemblyName));
        })
        .AddOperationalStore(opts =>
        {
            opts.ConfigureDbContext = builder =>
                builder.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(assemblyName));
        });

        var adminSection = Configuration.GetSection("DefaultAdmin");
        services.Configure<AdminUserOptions>(adminSection);

        services.AddTransient<IProfileService, ProfileService>();

        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();

        app.UseIdentityServer();
        app.UseCookiePolicy(new() { MinimumSameSitePolicy = SameSiteMode.Lax });
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }

    public void InitializeDatabase(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var grantContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
        var configContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        
        appContext.Database.Migrate();
        grantContext.Database.Migrate();
        configContext.Database.Migrate();
        
        if (!configContext.IdentityResources.Any())
        {
            foreach (var resource in Config.IdentityResources)
            {
                configContext.IdentityResources.Add(resource.ToEntity());
            }
        }

        if (!configContext.ApiScopes.Any())
        {
            foreach (var apiScope in Config.ApiScopes)
            {
                configContext.ApiScopes.Add(apiScope.ToEntity());
            }
        }
        
        if (!configContext.ApiResources.Any())
        {
            foreach (var apiResource in Config.ApiResources)
            {
                configContext.ApiResources.Add(apiResource.ToEntity());
            }
        }

        if (!configContext.Clients.Any())
        {
            string clientUrl = Configuration["ClientUrl"];
            foreach (var client in Config.GetClients(clientUrl))
            {
                configContext.Clients.Add(client.ToEntity());
            }
        }
        
        configContext.SaveChanges();
    }
}
