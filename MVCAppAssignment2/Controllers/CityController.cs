using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Controllers
{
    public class CityController : Controller
    {

        private readonly ICityService _myCityService;
        private readonly ICountryService _myCountryService;
        public CityController(ICityService theService, ICountryService moreService)  // Constuctor Dependency Injection
        {
            _myCityService = theService;
            _myCountryService = moreService;
        }


        //------------ Now handle the frontend stuff -----------------------------

        // GET: CityController
        public IActionResult Index()
        {
            Cities allCities = _myCityService.All();
            allCities.CountryList = _myCountryService.All().CountryList;    // To get the "parent" names of Countries
                                                                            // Where are our List of People??
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

                City newCity = _myCityService.Add(theCity); // We will get a new City object with working Id from this
                Country newCountry = _myCountryService.FindBy(theModel.City.CountryId);
                newCountry.Cities.Add(newCity); // and update this to the database
                _myCountryService.Edit(theModel.City.CountryId, newCountry);


                return RedirectToAction(nameof(Index)); // The RedirectToAction() method makes new requests, and URL in the
            }   // browser's address bar is updated with the generated URL by MVC. Standard Index will load.

            return View("Index", _myCityService.All());

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
            _myCityService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
