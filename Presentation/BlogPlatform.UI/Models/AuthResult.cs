namespace BlogPlatform.UI.Models;

public class AuthResult
{
    public string Token { get; set; }

    public IEnumerable<string> Errors { get; set; }
}
