using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace BlogPlatform.UI.Extensions;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string TokenKey = "accessToken";

    private readonly ILocalStorageService _localStorage;

    public JwtAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = await _localStorage.GetItemAsStringAsync(TokenKey);
        return BuildAuthenticationStateFromToken(token);
    }

    public async Task AuthenticateUserAsync(string userToken)
    {
        await _localStorage.SetItemAsync(TokenKey, userToken);
        var authState = BuildAuthenticationStateFromToken(userToken);
        base.NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task UnauthenticateAsync()
    {
        await _localStorage.RemoveItemAsync(TokenKey);
        var authState = BuildAuthenticationStateFromToken(string.Empty);
        base.NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    private static AuthenticationState BuildAuthenticationStateFromToken(string userToken)
    {
        ClaimsIdentity userIdentity = null;

        if (!string.IsNullOrWhiteSpace(userToken))
        {
            var userClaims = ParseClaimsFromJwt(userToken);
            userIdentity = new ClaimsIdentity(userClaims, "apiauth_type");
        }

        userIdentity ??= new ClaimsIdentity();
        ClaimsPrincipal principal = new(userIdentity);
        AuthenticationState authState = new(principal);
        return authState;
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        payload += (payload.Length % 4) switch
        {
            2 => "==",
            3 => "=",
            _ => string.Empty
        };

        byte[] jsonBytes = Convert.FromBase64String(payload);
        var pairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        var claims = pairs.Select(pair => new Claim(pair.Key, pair.Value.ToString()));

        Claim roleClaim = claims.FirstOrDefault(claim => claim.Type == "role");
        return claims.Append(new Claim(ClaimTypes.Role, roleClaim.Value));
    }
}
