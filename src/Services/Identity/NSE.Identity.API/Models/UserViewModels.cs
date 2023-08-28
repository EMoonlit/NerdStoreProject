using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NSE.Identity.API.Models;

public class UserRegister
{
    [Required(ErrorMessage = "The field {0} is required")]
    public string FullName { get; set; }
    
    [Required(ErrorMessage = "The field {0} is required")]
    public string Cpf { get; set; }
    
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

public class UserLoginResponse
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserTokenData UserTokenData { get; set; }
}

public class UserTokenData
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UserClaim> Claims { get; set; }
}

public class UserClaim
{
    public string Value { get; set; }
    public string Type { get; set; }
}