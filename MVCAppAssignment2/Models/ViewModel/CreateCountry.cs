using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class CreateCountry
    {

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Country Name")]
        public string Name { get; set; }
    }
}
