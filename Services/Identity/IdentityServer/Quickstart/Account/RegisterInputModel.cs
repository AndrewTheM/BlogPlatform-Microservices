using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Quickstart.Account;

public class RegisterInputModel
{
    [Required]
    [MinLength(3)]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Password confirmation doesn't match.")]
    public string PasswordConfirmation { get; set; }

    public string ReturnUrl { get; set; }
}
