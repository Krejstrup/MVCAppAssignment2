using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Controllers
{
    public class PeopleController : Controller
    {
        IPeopleService _myService;
        ICityService _myCityService;

        public PeopleController(IPeopleService theService, ICityService cityService)  // Constuctor Dependency Injection
        {
            _myService = theService;
            _myCityService = cityService;
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
            People allThePeopleAndCities = _myService.All();
            //Cities everCity = _myCityService.All();
            //allThePeopleAndCities.CityList = everCity.CityList;
            allThePeopleAndCities.CityList = _myCityService.All().CityList; // Used for creating a New Person
            return View(allThePeopleAndCities);
        }


        /// <summary>
        /// Create uses the create <form> to create a new person.
        /// </summary>
        /// <param name="theModel">The data model containg a person.</param>
        /// <returns>Return to Index page with all the People in the Data Model.</returns>
        [HttpPost]
        public IActionResult Create(People theModel)
        {
            if (ModelState.IsValid)
            {
                theModel.Person.CityId = theModel.TheTown;// CityId;
                _myService.Add(theModel.Person);        // send up the CreatePerson class data
                return RedirectToAction(nameof(Index)); // The RedirectToAction() method makes new requests, and URL in the
            }   // browser's address bar is updated with the generated URL by MVC. Standard Index will load.

            return View("Index", _myService.All());

        }



        /// <summary>
        /// Removes the selected person indexed by its Id.
        /// </summary>
        /// <param name="Id">The unique Id of this person.</param>
        /// <returns>Redirect to the Index to redraw the whole page again.</returns>
        [HttpGet]
        public IActionResult Remove(int Id)
        {
            _myService.Remove(Id);
            return RedirectToAction(nameof(Index));
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
                theModel = _myService.FindBy(theModel);
            }
            // If the filter string is empty: return all.
            else
            {
                theModel = _myService.All();
            }
            Cities everCity = _myCityService.All();
            theModel.CityList = everCity.CityList;

            // The Model state has to be cleared before returning to Index!
            ModelState.Clear();



            return View("Index", theModel);
        }

    }
}
