using BlogPlatform.UI.Models;

namespace BlogPlatform.UI.Services.Contracts;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(UserCredentials credentials);

    Task<AuthResult> RegisterAsync(User user);
}
