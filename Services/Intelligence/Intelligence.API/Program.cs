using BlogPlatform.Shared.Logging;
using Polly.Extensions.Http;
using Polly;
using Serilog;
using Intelligence.API.Services;
using Intelligence.API.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(SerilogHelpers.Configure);

builder.Services.AddScoped<LoggingDelegatingHandler>();

var section = builder.Configuration.GetRequiredSection("ContentManager");
builder.Services.AddHttpClient<IContentService, ContentService>(client =>
{
    client.BaseAddress = new Uri(section["Endpoint"]);
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", section["ApiKey"]);
})
.AddHttpMessageHandler<LoggingDelegatingHandler>()
.AddPolicyHandler(RetryPolicy())
.AddPolicyHandler(CircuitBreakerPolicy());

const string scheme = JwtBearerDefaults.AuthenticationScheme;
builder.Services.AddAuthentication(scheme)
    .AddJwtBearer(scheme, options =>
    {
        options.Authority = builder.Configuration["IdentityUrl"];
        options.Audience = "intelligenceApi";
        options.RequireHttpsMetadata = false;
    });
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
{
    return HttpPolicyExtensions.HandleTransientHttpError()
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: retry => TimeSpan.FromSeconds(Math.Pow(retry, 2)),
            onRetry: (exception, retryCount, context) =>
            {
                Log.Error($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
            });
}

static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 3,
            durationOfBreak: TimeSpan.FromSeconds(30));
}