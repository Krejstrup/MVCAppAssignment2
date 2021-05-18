using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class AccountRegister
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(20, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(25, MinimumLength = 6)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        [Compare("Password")]           // Compare with the Password functionality...
        public string ConfPassword { get; set; }

        // See also:
        // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio

    }
}
