using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class Login
    {

        [Required]
        [Display(Name = "User Name")]
        [StringLength(20, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
