namespace BlogPlatform.UI.Helpers.Contracts;

public interface IApiClient
{
    HttpClient HttpClient { get; set; }

    Task<T> SendGetApiRequestAsync<T>(string endpoint);

    Task<TResponse> SendPostApiRequestWithResultAsync<TRequest, TResponse>(
        string endpoint, TRequest body, bool ensureSuccess = true);

    Task SendModifyingApiRequestAsync<T>(HttpMethod method, string endpoint, T body);

    Task SendDeleteRequestAsync(string endpoint);

    Task EnsureAuthorizationHeader();
}
