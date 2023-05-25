using Blazored.LocalStorage;
using BlogPlatform.Shared.Logging;
using BlogPlatform.UI.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Localization;
using MudBlazor;
using MudBlazor.Services;
using Serilog;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SerilogHelpers.Configure);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddMudServices(config =>
{
    var snackbarConfig = config.SnackbarConfiguration;
    snackbarConfig.PositionClass = Defaults.Classes.Position.TopCenter;
    snackbarConfig.PreventDuplicates = false;
    snackbarConfig.ShowTransitionDuration = 200;
    snackbarConfig.HideTransitionDuration = 200;
});

//builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddApiServices(builder.Configuration["ApiGateway"]);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedUICultures = new List<CultureInfo> { new("en"), new("uk"), new("ru") };
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.Authority = builder.Configuration["IssuerUrl"];
    options.MetadataAddress = $"{builder.Configuration["IdentityUrl"]}/.well-known/openid-configuration";
    options.RequireHttpsMetadata = false;

    options.Events.OnRedirectToIdentityProvider = context =>
    {
        context.ProtocolMessage.IssuerAddress = $"{options.Authority}/connect/authorize";
        return Task.CompletedTask;
    };

    options.Events.OnAccessDenied = context =>
    {
        context.HandleResponse();
        context.Response.Redirect("/");
        return Task.CompletedTask;
    };

    options.ClientId = "ui-client";
    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
    options.ResponseType = "code";
    options.UsePkce = true;

    options.Scope.Remove("profile");
    options.Scope.Add("openid");
    options.Scope.Add("email");
    options.Scope.Add("roles");
    options.ClaimActions.MapUniqueJsonKey("role", "role", "role");

    options.Scope.Add("posts");
    options.Scope.Add("comments");
    options.Scope.Add("files");
    options.Scope.Add("accounts");

    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;

    options.TokenValidationParameters = new()
    {
        RoleClaimType = "role"
    };
});

builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStaticFiles();

app.UseCookiePolicy(new() { MinimumSameSitePolicy = SameSiteMode.Lax });

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();
