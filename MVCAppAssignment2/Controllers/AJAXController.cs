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

            if (onePerson != null)
            {
                return PartialView("_OnePersonPartial", onePerson);
            }


            return PartialView("_AllPersonsPartial", _myService.All());
        }


        [HttpPost]
        public IActionResult About(int Id) // WORKS!!
        {
            Person onePerson = _myService.FindBy(Id);

            if (onePerson != null)
            {
                //return PartialView("_personThumbnail", onePerson);
                return PartialView("_PersonAboutPartial", onePerson);
            }
            return PartialView("_AllPersonsPartial", _myService.All());
        }


        [HttpPost]
        public IActionResult NotAbout(int Id) // WORKS??
        {
            Person onePerson = _myService.FindBy(Id);

            if (onePerson != null)
            {
                //return PartialView("_personThumbnail", onePerson);
                return PartialView("_PersonPartial", onePerson);
            }
            return PartialView("_AllPersonsPartial", _myService.All());
        }

        [HttpPost]
        public IActionResult Remove(int Id) // WORKS!!
        {
            Person aPerson = _myService.FindBy(Id);

            if (aPerson == null)
            {
                return NotFound();
            }

            if (_myService.Remove(Id))
            {
                return Ok(Id);
            }

            return BadRequest();
            //return RedirectToAction(nameof(Index));

        }




        [HttpPost]
        public IActionResult Edit(Person aPerson) // WORKS NOT QUITE YET!!
        {
            //Person aPerson = _myService.FindBy(Id);

            if (aPerson == null)
            {
                return NotFound();
            }

            if (_myService.Edit(aPerson.Id, aPerson) != null)
            {
                return Ok(aPerson.Id);
            }

            return BadRequest();
            //return RedirectToAction(nameof(Index));

        }
    }
}
