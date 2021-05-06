using System.ComponentModel.DataAnnotations;

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

        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(30)]
        public string Country { get; set; }

        //[ForeignKey("InCity")]
        //public int CityId { get; set; }


        //public City InCity { get; set; }
    }
}
