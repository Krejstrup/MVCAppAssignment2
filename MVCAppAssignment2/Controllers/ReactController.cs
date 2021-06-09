using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCAppAssignment2.Controllers
{

    [EnableCors("ReactPolicy")]
    [Route("api/[controller]")]
    [ApiController]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ReactController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {

        IPeopleService _myPeopleService;
        ICityService _myCityService;
        ILanguageService _myLangService;
        ICountryService _myCountryService;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ReactController(IPeopleService peopleService, ICityService cityService, ILanguageService langService, ICountryService countryService)  // Constuctor Dependency Injection
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _myPeopleService = peopleService;
            _myCityService = cityService;
            _myLangService = langService;
            _myCountryService = countryService;
        }
        //----------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets a person from API to put into the database. Checks the ModelState for validation.
        /// </summary>
        /// <param name="person">The person data that should be appended to the database.</param>
        /// <returns>Returns the person if ok, otherwise a BadRequest for the Client.</returns>
        [HttpPost]
        public ActionResult<Person> Create([FromBody] CreatePerson person)  // TODO: Model binding
        {
            if (person is null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            if (ModelState.IsValid)
            {
                Person newPerson = _myPeopleService.Add(person);
                // remove other persons in the City list
                //
                newPerson.InCity.Peoples = null;
                if (newPerson != null)
                {
                    return Created("", newPerson);
                }
            }

            return BadRequest();
        }


        /// <summary>
        /// API Controller Get: Fetches the whole list of people in the database.
        /// </summary>
        /// <returns>A List of Person.</returns>
        [HttpGet]
        public List<Person> Get()
        {
            List<Person> allPeople = _myPeopleService.ApiAll().PersonList; // contains the full list of people.

            return allPeople;
        }



        /// <summary>
        /// GetCities fetches all the Cities stored in the Database, without
        /// the long list of persons in it.
        /// </summary>
        /// <returns>Returns a list of Cities.</returns>
        [HttpGet("Cities")]
        public List<City> GetCities()
        {
            return _myCityService.ApiAll().CityList;

        }


        /// <summary>
        /// GetCities fetches all the Languages stored in the Database, without
        /// connections to other persons in it.
        /// </summary>
        /// <returns>Returns a list of Languages.</returns>
        [HttpGet("Languages")]
        public List<Language> GetLanguages()
        {
            return _myLangService.ApiAll().LanguageList;
        }

        /// <summary>
        /// GetCountries fetches all the Countries stored in the Database, without
        /// connections to other Cities in it.
        /// </summary>
        /// <returns>Returns a list of Languages.</returns>
        [HttpGet("Countries")]
        public List<Country> GetCountries()
        {
            List<Country> theWholeLot = _myCountryService.All().CountryList;
            foreach (Country aCountry in theWholeLot)
            {
                aCountry.Cities = null;
            }

            return theWholeLot;
        }



        /// <summary>
        /// API Controller GetById: Fetches on person pointed out by its unique id.
        /// </summary>
        /// <param name="id">The unique Id of this person.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Person GetById(int id)
        {
            Person thePerson = _myPeopleService.FindBy(id);

            if (thePerson == null)
            {
                Response.StatusCode = 400;
                return null;
            }

            thePerson.InCity.Peoples = null;
            foreach (PersonLanguage language in thePerson.PersonLanguages)
            {
                language.Person = null;
            }
            return thePerson;
        }




        /// <summary>
        /// Deletes one of the Persons in the Database. The service checks if the Person exists.
        /// </summary>
        /// <param name="id">The Unique Id of the person that should be removed.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            if (_myPeopleService.Remove(id))
            {
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
            }

        }
    }
}
