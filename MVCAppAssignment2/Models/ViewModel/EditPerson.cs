using MVCAppAssignment2.Models.Data;
using System.Collections.Generic;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class EditPerson  //-------- This is a ViewModel -----------------
    {
        //--- Basic person data -------------------------------------

        public Person Person { get; set; }

        public List<Language> LanguageList { get; set; }

        public List<City> CityList { get; set; }
    }
}
