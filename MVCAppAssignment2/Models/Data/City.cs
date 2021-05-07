using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAppAssignment2.Models.Data
{
    public class City
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }


        public List<Person> People { get; set; }

        [ForeignKey("Country")]     // With this key we will tell the Eager loding where to place the data
        public int CountryId { get; set; }

        // One
        public Country Country { get; set; }
    }
}
