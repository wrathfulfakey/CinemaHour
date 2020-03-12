using System.ComponentModel.DataAnnotations;

namespace CinemaHour.Services.ViewModels
{
    public class UserRegisterInputViewModel
    {
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Your first name must be between 3 and 20 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Your last name must be between 3 and 20 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 20 characters.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
