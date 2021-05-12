using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.Data
{
    public class Language
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }



        public List<PersonLanguage> PersonLanguages { get; set; }   // To the Assossiation table

    }
}
