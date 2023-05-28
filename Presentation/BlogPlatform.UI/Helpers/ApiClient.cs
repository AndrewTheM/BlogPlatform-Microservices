using BlogPlatform.UI.Helpers.Contracts;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace BlogPlatform.UI.Helpers;

public class ApiClient : IApiClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpClient HttpClient { get; set; }

    public ApiClient(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        HttpClient = new();
    }

    public async Task<T> SendGetApiRequestAsync<T>(string endpoint)
    {
        await EnsureAuthorizationHeader();
        var urlWithCulture = AppendCulture(endpoint);
        var response = await HttpClient.GetAsync(urlWithCulture);
        response.EnsureSuccessStatusCode();

        using var contentStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<T>(contentStream, options: new()
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<TResponse> SendPostApiRequestWithResultAsync<TRequest, TResponse>(
        string endpoint, TRequest body, bool ensureSuccess = true)
    {
        await EnsureAuthorizationHeader();
        var urlWithCulture = AppendCulture(endpoint);
        var bodyJson = JsonSerializer.Serialize(body);
        using StringContent jsonContent = new(bodyJson, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync(urlWithCulture, jsonContent);
        if (ensureSuccess)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(message, null, HttpStatusCode.BadRequest);
            }

            response.EnsureSuccessStatusCode();
        }

        using var contentStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<TResponse>(contentStream, options: new()
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task SendModifyingApiRequestAsync<T>(HttpMethod method, string endpoint, T body)
    {
        var allowedMethods = new HashSet<HttpMethod>() { HttpMethod.Post, HttpMethod.Put, HttpMethod.Patch };

        if (!allowedMethods.Contains(method))
            throw new ArgumentException("HTTP method not allowed.", nameof(method));

        await EnsureAuthorizationHeader();
        var bodyJson = JsonSerializer.Serialize(body);
        using var jsonContent = new StringContent(bodyJson, Encoding.UTF8, MediaTypeNames.Application.Json);

        var request = new HttpRequestMessage(method, endpoint) { Content = jsonContent };
        var response = await HttpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(message, null, HttpStatusCode.BadRequest);
        }

        response.EnsureSuccessStatusCode();
    }

    public async Task SendDeleteRequestAsync(string endpoint)
    {
        await EnsureAuthorizationHeader();
        var response = await HttpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
    }

    public async Task EnsureAuthorizationHeader()
    {
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        HttpClient.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);
    }

    private static string AppendCulture(string url)
    {
        return $"{url}{(url.Contains('?') ? '&' : '?')}culture={CultureInfo.CurrentUICulture.Name}";
    }
}
