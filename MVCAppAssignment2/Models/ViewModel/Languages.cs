using MVCAppAssignment2.Models.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class Languages
    {

        public int LanguageId { get; set; }

        [Display(Name = "A Language")]
        public CreateLanguage Language { get; set; }

        [Display(Name = "Filter")]
        public string filter { get; set; }




        [Display(Name = "List of Persons")]
        public List<Person> PersonList { get; set; }

        [Display(Name = "List of Languages")]
        public List<Language> LanguageList { get; set; }

        public Languages()
        {
            LanguageList = new List<Language>();
            PersonList = new List<Person>();
        }


    }
}
