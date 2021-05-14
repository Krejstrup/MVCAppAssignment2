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


        //public int PeopleId { get; set; }     Detta behövs inte eftersom det redan finns länk från Person!
        //[ForeignKey("PeopleId")]     // With this key we will tell the Eager loding where to place the data

        public List<Person> Peoples { get; set; }    // Many

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]     // With this key we will tell the Eager loding where to place the data
        public Country Country { get; set; }        // One
    }
}
