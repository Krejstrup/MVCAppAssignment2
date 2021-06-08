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
    public class People  //-------- This is a ViewModel -----------------
    {
        /// <summary>
        /// PersonList contains a List of all the persons in the Database
        /// </summary>
        [Display(Name = "List of Persons")]
        public List<Person> PersonList { get; set; }

        /// <summary>
        /// CityList contains a List of all the cities in the Database
        /// </summary>
        [Display(Name = "List of Cities")]
        public List<City> CityList { get; set; }




        /// <summary>
        /// Person is a ViewModel to create a new person
        /// </summary>
        [Display(Name = "A Person")]
        public CreatePerson Person { get; set; }

        /// <summary>
        /// filter is used to search a specific name
        /// </summary>
        [Display(Name = "Filter")]
        public string filter { get; set; }

        /// <summary>
        /// CityId is used to point out the location of the person
        /// </summary>
        public int CityId { get; set; }    // This was previously "public int CityId { get; set; }"



        /// <summary>
        /// Constructor that creates a new empty list of persons.
        /// A list of Cities is also used. When used, go through all persons
        /// and pick the cities included to put in this list.
        /// </summary>
        public People()
        {
            PersonList = new List<Person>();
            CityList = new List<City>();
            Person = new CreatePerson();
        }

    }
}
