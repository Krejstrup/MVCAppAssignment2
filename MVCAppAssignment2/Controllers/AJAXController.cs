using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Controllers
{

    public class AJAXController : Controller
    {
        private readonly IPeopleService _myPService;
        private readonly ICityService _myCityService;
        private readonly ICountryService _myCountryService;
        private readonly ILanguageService _myLanguageService;

        public AJAXController(IPeopleService theService, ICityService aCityService, ICountryService aCountryService, ILanguageService aLangService)//constuctor injection
        {
            _myPService = theService;
            _myCityService = aCityService;
            _myCountryService = aCountryService;
            _myLanguageService = aLangService;
        }




        /// <summary>
        /// Index loads the entire page with a view.
        /// </summary>
        /// <returns>The standard view to display.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// AllPersons returns the list of all persons in the database/memory.
        /// </summary>
        /// <returns>The list of all the persons in the database/memory.</returns>
        [HttpGet]
        public IActionResult AllPersons()
        {
            // Send down all the persons in this Model
            People allThePeopleAndCities = _myPService.All();
            Cities everCity = _myCityService.All();
            allThePeopleAndCities.CityList = everCity.CityList;

            return PartialView("_AllPersonsPartial", allThePeopleAndCities);
        }


        /// <summary>
        /// Filter is only using the person Id to filter out one person.
        /// </summary>
        /// <param name="Id">The person to look up.</param>
        /// <returns>Returns a partial view of a person on success, otherwise NotFound (404).</returns>
        [HttpPost]
        public IActionResult Filter(int Id)
        {
            Person onePerson = _myPService.FindBy(Id);  // Person does not have room for Cities...

            if (onePerson != null)
            {
                //Cities everCity = _myCityService.All();
                //allThePeopleAndCities.CityList = everCity.CityList;
                return PartialView("_OnePersonPartial", onePerson);
            }
            return NotFound();
        }


        /// <summary>
        /// About opens up a partial view for the person clicked. The person partial view is replaced.
        /// </summary>
        /// <param name="Id">The Id pointing out the person to open about info with.</param>
        /// <returns>Returns the partial view of a personal Card info.</returns>
        [HttpPost]
        public IActionResult Edit(int Id) // WORKS!!
        {
            Person onePerson = _myPService.FindBy(Id);

            if (onePerson != null)
            {
                return PartialView("_PersonEditPartial", onePerson);
            }
            return PartialView("_AllPersonsPartial", _myPService.All());
        }



        /// <summary>
        /// NotAbout closes the info partial view for the person clicked. The person partial view is replaced.
        /// </summary>
        /// <param name="Id">The Id pointing out the person view to replace About with.</param>
        /// <returns>Returns the standard partial view of a person.</returns>
        [HttpPost]
        public IActionResult CloseEdit(int Id) // WORKS!!
        {
            Person onePerson = _myPService.FindBy(Id);

            if (onePerson != null)
            {
                EditPerson returnPerson = new EditPerson()
                {
                    Person = onePerson
                };

                // Now fetch and refill the lists of cities and languages before returning.
                Cities getCities = _myCityService.All();
                returnPerson.CityList = getCities.CityList;
                Languages getLanguages = _myLanguageService.All();
                returnPerson.LanguageList = getLanguages.LanguageList;

                return PartialView("_PersonPartial", returnPerson);
            }
            return PartialView("_AllPersonsPartial", _myPService.All());
        }


        /// <summary>
        /// Remove deletes the given person from the database/memory. And deletes the representing personal row.
        /// </summary>
        /// <param name="Id">The Id of the person to be removed.</param>
        /// <returns>Returns Ok (200) if success otherwise BadRequest or NotFound (404)</returns>
        [HttpPost]
        public IActionResult Remove(int Id) // WORKS!!
        {
            Person aPerson = _myPService.FindBy(Id);

            if (aPerson == null)
            {
                return NotFound();
            }

            if (_myPService.Remove(Id))
            {
                return Ok(Id);
            }

            return BadRequest();
        }



        /// <summary>
        /// Edit is not quite finished yet!!!
        /// </summary>
        /// <param name="aPerson">The Person to be edited.</param>
        /// <returns>Returns Ok (200) and data for the person Id on success, otherwise BadRequest or NotFound (404)</returns>
        [HttpPost]
        public IActionResult EditPerson(Person aPerson) // WORKS NOT QUITE YET!!
        {

            if (aPerson == null)
            {
                return NotFound();
            }

            if (_myPService.Edit(aPerson.Id, aPerson) != null)
            {
                return Ok(aPerson.Id);
            }

            return BadRequest();
        }



        [HttpPost]
        public IActionResult EditFname(int Id, EditPerson changePerson) // WORKS NOT QUITE YET!!
        {
            Person aPerson = _myPService.FindBy(Id);

            if (aPerson == null)
            {
                return NotFound();
            }

            if (_myPService.Edit(aPerson.Id, changePerson) != null)
            {
                return Ok(aPerson.Id);
            }

            return BadRequest();
        }


    }
}
