using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Controllers
{
    public class PeopleController : Controller
    {
        IPeopleService _myPersService;
        ICityService _myCityService;
        ILanguageService _myLangService;

        public PeopleController(IPeopleService theService, ICityService cityService, ILanguageService langService)  // Constuctor Dependency Injection
        {
            _myPersService = theService;
            _myCityService = cityService;
            _myLangService = langService;
        }



        // --------------- Lets begin organizing the Web functions ---------------------------


        /// <summary>
        /// The Index page gets a Data-View-Model of all the People in a List<>.
        /// The page is totally redrawn.
        /// </summary>
        /// <returns>The build View by full list.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            People allThePeopleAndCities = _myPersService.All();
            allThePeopleAndCities.CityList = _myCityService.All().CityList; // Used for creating a New Person
            return View(allThePeopleAndCities);
        }


        /// <summary>
        /// Create uses the create <form> to create a new person. The Person is also added
        /// to the person list of the city.
        /// </summary>
        /// <param name="theModel">The data model containg a person.</param>
        /// <returns>Return to Index page with all the People in the Data Model.</returns>
        [HttpPost]
        public IActionResult Create(People theModel)
        {
            if (ModelState.IsValid)
            {
                Person newPerson = _myPersService.Add(theModel.Person);     // Adds the person to database and require its Id

                City updateCity = _myCityService.FindBy(theModel.Person.CityId);   // Get the City
                updateCity.Peoples.Add(newPerson);                                 // Add newPerson to City List of persons
                _myCityService.Edit(theModel.Person.CityId, updateCity);           // Edit the City in te database

                return RedirectToAction(nameof(Index));     // The RedirectToAction() method makes new requests, and URL in the
            }   // browser's address bar is updated with the generated URL by MVC. Standard Index will load.


            return View("Index", _myPersService.All());

        }






        /// <summary>
        /// Edit is directing to edit a person.
        /// </summary>
        /// <param name="Id">The unique Id of the person to be Edited.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// EditLang is using the View PeopleLang to edit a persons Laguages.
        /// The method is usually called from JS function Edit(id) in AJAX by _personPartial Partial View.
        /// </summary>
        /// <param name="Id">The unique Id of the person to be Edited.</param>
        /// <returns></returns>
        //[HttpPost]
        public IActionResult LangEdit(int Id)   // Called by AJAX
        {
            Person originalPerson = _myPersService.FindBy(Id);

            EditPerson newPerson = new EditPerson()
            {
                Person = originalPerson
            };

            newPerson.LanguageList = _myLangService.All().LanguageList;
            newPerson.CityList = _myCityService.All().CityList;
            return View("PeopleLang", newPerson);
        }


        //---------Add a binding between the person and the language----------------------
        public IActionResult AddLang(int perId, int langId)
        {

            Person originalPerson = _myPersService.FindBy(perId);
            //Create a EditPerson object and fill it up, then send it to the PeopleLang View
            if (originalPerson == null)
            {
                return RedirectToAction("Index");   // Throw it back if it didn't work
            }

            //======TODO: ================================================================//
            // Check for allready contains or remove the Add-choice for doubles..         //
            //----------------------------------------------------------------------------//
            bool found = false;
            foreach (PersonLanguage item in originalPerson.PersonLanguages)
            {
                if (item.LanguageId == langId)
                {
                    found = true;
                    break;
                }
            }
            if (!found) _myPersService.AddLanguageToPerson(perId, langId);


            EditPerson returnPerson = new EditPerson()
            {
                Person = originalPerson
            };

            returnPerson.LanguageList = _myLangService.All().LanguageList;
            returnPerson.CityList = _myCityService.All().CityList;

            return View("PeopleLang", returnPerson);
        }


        public IActionResult RemoveLang(int perId, int langId)
        {

            // Create a EditPerson object to check if exist
            Person originalPerson = _myPersService.FindBy(perId);
            if (originalPerson == null)
            {
                return RedirectToAction("PeopleLang");
            }
            _myPersService.RemoveLanguageFromPerson(perId, langId);

            EditPerson returnPerson = new EditPerson()
            {
                Person = originalPerson
            };
            returnPerson.LanguageList = _myLangService.All().LanguageList;
            returnPerson.CityList = _myCityService.All().CityList;

            return View("PeopleLang", returnPerson);
        }





        /// <summary>
        /// Removes the selected person indexed by its Id. The person must be removed in the City List aswell.
        /// </summary>
        /// <param name="Id">The unique Id of this person.</param>
        /// <returns>Redirect to the Index to redraw the whole page again.</returns>
        [HttpGet]
        public IActionResult Remove(int Id)     // TODO: Kolla om detta är rekursivt och att hen försvinner från City oxå!
        {
            //Person thisPerson = _myPersService.FindBy(Id);

            //City theCity = thisPerson.InCity;
            //theCity.Peoples.Remove(thisPerson);

            _myPersService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }





        /// <summary>
        /// Filter gets a Data model that contains a filter string and
        /// a CreatePerson model. Both can be used as filtering.
        /// Filter string search every field and the Person will be more specific.
        /// THe ModelState has to be cleared before return!
        /// </summary>
        /// <param name="theModel"></param>
        /// <returns>Returns to the Index page with a new filtered Data Model.</returns>
        [HttpPost]
        public IActionResult Filter(People theModel)
        {
            // At the moment there is no <form> that takes the Person data for filtering.

            // First check if the filter string contains anything
            if (theModel.filter != "" && theModel.filter != null)
            {
                theModel = _myPersService.FindBy(theModel);
            }
            // If the filter string is empty: return all.
            else
            {
                theModel = _myPersService.All();
            }
            Cities everCity = _myCityService.All();
            theModel.CityList = everCity.CityList;

            // The Model state has to be cleared before returning to Index!
            ModelState.Clear();



            return View("Index", theModel);
        }

    }
}
