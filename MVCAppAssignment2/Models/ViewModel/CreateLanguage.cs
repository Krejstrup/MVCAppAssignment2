using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class CreateLanguage
    {

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Language Name")]
        public string Name { get; set; }



        public CreateLanguage() { }

    }
}
