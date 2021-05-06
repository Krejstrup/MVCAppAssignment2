using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class CreateCity
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "City Name")]
        public string Name { get; set; }

        public int CountryId { get; set; }
    }
}
