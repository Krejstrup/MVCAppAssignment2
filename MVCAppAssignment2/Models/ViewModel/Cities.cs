using MVCAppAssignment2.Models.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class Cities
    {
        [Display(Name = "List of Cities")]
        public List<City> CityList { get; set; }

        [Display(Name = "List of Countries")]
        public List<Country> CountryList { get; set; }

        [Display(Name = "A City")]
        public CreateCity City { get; set; }

        [Display(Name = "Filter")]
        public string filter { get; set; }

        public int CountryId { get; set; }


        public Cities()
        {
            CityList = new List<City>();
            CountryList = new List<Country>();
            City = new CreateCity();
        }


    }
}
