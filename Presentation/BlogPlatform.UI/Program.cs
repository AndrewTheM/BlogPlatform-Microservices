using Blazored.LocalStorage;
using BlogPlatform.UI.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Localization;
using MudBlazor;
using MudBlazor.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddApiServices(builder.Configuration["ApiGateway"]);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedUICultures = new List<CultureInfo> { new("en"), new("uk"), new("ru") };
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

app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
