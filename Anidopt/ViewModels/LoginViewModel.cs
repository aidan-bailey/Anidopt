using System.ComponentModel.DataAnnotations;

namespace Anidopt.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email address required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
