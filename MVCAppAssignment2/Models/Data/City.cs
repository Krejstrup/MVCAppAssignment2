using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.Data
{
    public class City
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }


        //public List<Person> People { get; set; }

        //[ForeignKey("Country")]
        public int CountryId { get; set; }

        //public Country Country { get; set; }
    }
}
