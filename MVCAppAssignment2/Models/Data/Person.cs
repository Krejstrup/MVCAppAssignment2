using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAppAssignment2.Models.Data
{

    /// <summary>
    /// Person is a database data model of a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The unique Id of this person.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// This persons first name.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        /// <summary>
        /// This persons last name.
        /// </summary>
        [MaxLength(30)]
        public string LastName { get; set; }

        /// <summary>
        /// This persons phone number.
        /// </summary>
        [MaxLength(30)]
        public string Phone { get; set; }

        /// <summary>
        /// This persons location. This is a Id to an One-to-Many database relation.
        /// </summary>
        [ForeignKey("InCity")]     // With this key we will tell the Eager loding where to place the data
        public int CityId { get; set; }

        /// <summary>
        /// This persons City location that contains name of City. This is an One-to-Many database relation.
        /// </summary>
        public City InCity { get; set; }    // One


        /// <summary>
        /// This persons Languages. This is a Many-to-Many database relation.
        /// </summary>
        public List<PersonLanguage> PersonLanguages { get; set; }   // To the Assossiation table
    }
}
