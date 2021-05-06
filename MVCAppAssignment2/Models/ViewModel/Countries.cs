using MVCAppAssignment2.Models.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class Countries
    {

        [Display(Name = "List of Countries")]
        public List<Country> CountryList { get; set; }

        [Display(Name = "A Country")]
        public CreateCountry Country { get; set; }

        [Display(Name = "Filter")]
        public string filter { get; set; }  // My first idea was to use the CreatePerson, and to use a <form> as filter.
                                            // I have to have the time for this implementation too...

        /// <summary>
        /// Constructor that creates a new empty list of persons.
        /// </summary>
        public Countries()
        {
            CountryList = new List<Country>();
            Country = new CreateCountry();
        }
    }
}
