using System.ComponentModel.DataAnnotations;

namespace NSE.Identity.API.Models;

public class UserRegister
{
    [Required(ErrorMessage = "The field {0} is required")]
    [EmailAddress(ErrorMessage = "Field {0} is in invalid format")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters long", MinimumLength = 6)]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string ConfirmationPassword { get; set; }
}

public class UserLogin
{
    [Required(ErrorMessage = "The field {0} is required")]
    [EmailAddress(ErrorMessage = "Field {0} is in invalid format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters long", MinimumLength = 6)]
    public string Password { get; set; }
}