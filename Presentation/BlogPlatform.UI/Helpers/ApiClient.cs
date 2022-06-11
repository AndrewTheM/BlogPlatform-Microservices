using Blazored.LocalStorage;
using BlogPlatform.UI.Helpers.Contracts;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace BlogPlatform.UI.Helpers;

public class ApiClient : IApiClient
{
    private readonly ILocalStorageService _localStorage;

    public HttpClient HttpClient { get; set; }

    public ApiClient(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        HttpClient = new();
    }

    public async Task<T> SendGetApiRequestAsync<T>(string endpoint)
    {
        await EnsureAuthorizationHeader();
        string urlWithCulture = AppendCulture(endpoint);
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
        string urlWithCulture = AppendCulture(endpoint);
        string bodyJson = JsonSerializer.Serialize(body);
        using StringContent jsonContent = new(bodyJson, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync(urlWithCulture, jsonContent);
        if (ensureSuccess)
        {
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
        HashSet<HttpMethod> allowedMethods = new() { HttpMethod.Post, HttpMethod.Put, HttpMethod.Patch };

        if (!allowedMethods.Contains(method))
        {
            throw new ArgumentException("HTTP method not allowed.", nameof(method));
        }

        await EnsureAuthorizationHeader();
        string bodyJson = JsonSerializer.Serialize(body);
        using StringContent jsonContent = new(bodyJson, Encoding.UTF8, "application/json");

        HttpRequestMessage request = new(method, endpoint) { Content = jsonContent };
        var response = await HttpClient.SendAsync(request);
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
        string accessToken = await _localStorage.GetItemAsync<string>("accessToken");
        HttpClient.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);
    }

    private static string AppendCulture(string url)
    {
        return $"{url}{(url.Contains('?') ? '&' : '?')}culture={CultureInfo.CurrentUICulture.Name}";
    }
}
