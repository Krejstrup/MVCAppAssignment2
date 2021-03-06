using MVCAppAssignment2.Models.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{

    /// <summary>
    /// This is the data template model for the _ViewModel_ displaying the persons in the list.
    /// Fetch the data from the InMemoryPeopleRepro.
    /// filter is to be able to filtering the listing in the View.
    /// </summary>
    public class People             //ViewModel
    {
        [Display(Name = "List of Persons")]
        public List<Person> PersonList { get; set; }

        [Display(Name = "A Person")]
        public CreatePerson Person { get; set; }

        [Display(Name = "Filter")]
        public string filter { get; set; }       // My first idea was to use the CreatePerson, and to use a <form> as filter.
                                                 // I have to have the time for this implementation too...

        /// <summary>
        /// Constructor that creates a new empty list of persons.
        /// </summary>
        public People()
        {
            PersonList = new List<Person>();
            Person = new CreatePerson();
        }

    }
}
