using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    /// <summary>
    /// CreatePerson is a ViewModel för freating a person, with all details needed.
    /// </summary>
    public class CreatePerson
    {
        /// <summary>
        /// Tis persons First Name
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Tis persons Last Name
        /// </summary>
        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        /// <summary>
        /// This persons Phone Number
        /// </summary>
        [StringLength(30)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        /// <summary>
        /// This persons City location
        /// </summary>
        [Required]
        [Display(Name = "CityId")]
        public int CityId { get; set; }

        /// <summary>
        /// Empty COnstructor for this class
        /// </summary>
        public CreatePerson() { }
    }
}
