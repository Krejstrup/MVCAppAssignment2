using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Controllers
{
    public class CityController : Controller
    {

        private readonly ICityService _myService;
        private readonly ICountryService _myCountryService;
        public CityController(ICityService theService, ICountryService moreService)  // Constuctor Dependency Injection
        {
            _myService = theService;
            _myCountryService = moreService;
        }



        // GET: CityController
        public IActionResult Index()
        {
            Cities allCities = _myService.All();
            allCities.CountryList = _myCountryService.All().CountryList;

            return View(allCities);
        }



        // GET: CityController/Details/5    === NOT IN USE YET ====
        public IActionResult Details(int id)
        {
            return View();
        }




        // POST: CityController/Create - for a MVC standard interface
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cities theModel)    // === WORKS FINE!
        {

            if (ModelState.IsValid)
            {

                CreateCity theCity = theModel.City;
                theCity.CountryId = theModel.CountryId;

                _myService.Add(theCity);        // set up the CreateCountry class data to database
                Country actualCountry = _myCountryService.FindBy(theModel.CountryId);
                // Nu har jag skapat en stad och hämtat landet det ligger i.
                // Stoppa in staden i landet:
                //actualCountry.Cities.Add(theCity);
                // Uppdatera repot med det uppdaterade landet:
                //_myCountryService.Edit(theModel.CountryId, actualCountry);

                return RedirectToAction(nameof(Index)); // The RedirectToAction() method makes new requests, and URL in the
            }   // browser's address bar is updated with the generated URL by MVC. Standard Index will load.

            return View("Index", _myService.All());

        }




        // GET: CityController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }




        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cities myCollection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





        // GET: CityController/Delete/5
        public IActionResult Delete(int id)
        {
            _myService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
