using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Accounts
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        //[ValidEmailDomain(allowedDomain: ".com", ErrorMessage = "Email domain must be .com")]
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
    }
}
