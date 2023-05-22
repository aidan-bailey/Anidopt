using System.ComponentModel.DataAnnotations;

namespace Anidopt.ViewModels;

public class RegistrationViewModel
{
    [Required(ErrorMessage = "Please enter First Name")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please enter LastName")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please enter password")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
    [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase      Alphabet, 1 Number and 1 Special Character")]
    public string Password { get; set; }

    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Please enter confirm password")]
    [Compare("Password", ErrorMessage = "Confirm password doesn't match")]
    [DataType(DataType.Password)]
    public string Confirmpwd { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
    public string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number")]
    [Required(ErrorMessage = "Phone Number Required!")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
    public string PhoneNumber { get; set; }
}

