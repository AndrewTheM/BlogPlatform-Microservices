using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Models;
using BlogPlatform.UI.Services.Contracts;

namespace BlogPlatform.UI.Services;

public class AuthService : IAuthService
{
    private readonly IApiClient _apiClient;

    public AuthService(IApiClient apiClient, HttpClient httpClient)
    {
        _apiClient = apiClient;
        _apiClient.HttpClient = httpClient;
    }

    public async Task<AuthResult> LoginAsync(UserCredentials credentials)
    {
        return await _apiClient.SendPostApiRequestWithResultAsync<UserCredentials, AuthResult>(
            endpoint: "users/login",
            body: credentials,
            ensureSuccess: false
        );
    }

    public async Task<AuthResult> RegisterAsync(User user)
    {
        return await _apiClient.SendPostApiRequestWithResultAsync<User, AuthResult>(
            endpoint: "users/register",
            body: user,
            ensureSuccess: false
        );
    }
}
