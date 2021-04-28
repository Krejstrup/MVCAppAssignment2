using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class CreatePerson   // A ViewModel for passing data for one person
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [StringLength(30)]
        [Display(Name = "City")]
        public string City { get; set; }



    }
}
