using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.Data
{

    /// <summary>
    /// The Language is used by a person to know of a personal level. It is assossiated as Many-to-Many in the database.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// This is the Unique Id of this language.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// This is the actual name of the language.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }


        /// <summary>
        /// This is the list to other Persons via the assossiation table
        /// </summary>
        public List<PersonLanguage> PersonLanguages { get; set; }   // To the Assossiation table

    }
}
