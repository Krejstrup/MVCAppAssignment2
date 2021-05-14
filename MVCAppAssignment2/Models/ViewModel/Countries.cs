using MVCAppAssignment2.Models.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class Countries
    {

        [Display(Name = "List of Countries")]
        public List<Country> CountryList { get; set; }  // A list of all the countries

        [Display(Name = "A Country")]
        public CreateCountry Country { get; set; }      // The ViewModel to create a new Country

        [Display(Name = "Filter")]
        public string Filter { get; set; }              // Filter string used to filter the selection


        // On create set up the Lists with new List objects.
        public Countries()
        {
            CountryList = new List<Country>();
            Country = new CreateCountry();
        }
    }
}
