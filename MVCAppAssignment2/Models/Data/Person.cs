using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAppAssignment2.Models.Data
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string Phone { get; set; }


        [ForeignKey("InCity")]     // With this key we will tell the Eager loding where to place the data
        public int CityId { get; set; }

        public City InCity { get; set; }    // One



        public List<PersonLanguage> PersonLanguages { get; set; }   // To the Assossiation table
    }
}
