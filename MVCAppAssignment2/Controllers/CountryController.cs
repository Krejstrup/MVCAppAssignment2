using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Controllers
{
    public class CountryController : Controller
    {

        private readonly ICountryService _myService;
        public CountryController(ICountryService theService)  // Constuctor Dependency Injection
        {
            _myService = theService;
        }




        // Standard startup Index page
        [HttpGet]
        public ActionResult Index()
        {
            return View(_myService.All());
        }


        [HttpGet]
        public IActionResult AllCountries() // WORKS?????????????????
        {
            // Send down all the persons in this Model
            Countries everyCountry = _myService.All(); // Använd en ViewModel

            return PartialView("_AllPersonsPartial", everyCountry);
        }






        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Countries theModel)
        {

            if (ModelState.IsValid)
            {
                _myService.Add(theModel.Country);        // set up the CreateCountry class data to database
                return RedirectToAction(nameof(Index)); // The RedirectToAction() method makes new requests, and URL in the
            }   // browser's address bar is updated with the generated URL by MVC. Standard Index will load.

            return View("Index", _myService.All());

        }






        // GET: CountryController/Delete/5      ==== WORKS FINE!
        public IActionResult Delete(int id)
        {
            _myService.Remove(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
