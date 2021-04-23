using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;



namespace MVCAppAssignment2.Controllers
{

    public class AJAXController : Controller
    {
        private readonly IPeopleService _myService = new PeopleService();




        [HttpGet]
        public IActionResult Index()       // WORKS!!
        {
            return View();
        }



        [HttpGet]
        public IActionResult AllPersons() // WORKS!!
        {
            // Send down all the persons in this Model
            People everyPerson = _myService.All();
            return PartialView("_AllPersonsPartial", everyPerson);
        }


        [HttpPost]
        public IActionResult Filter(int Id)
        {
            Person onePerson = _myService.FindBy(Id);

            return PartialView("_OnePersonPartial", onePerson);
        }


        [HttpPost]
        public IActionResult Remove(int Id) // WORKS!!
        {
            _myService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }




    }
}
