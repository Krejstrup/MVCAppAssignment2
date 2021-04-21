using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Controllers
{
    public class PeopleController : Controller
    {
        private IPeopleService MyService = new PeopleService();
        //CreatePerson addPerson = new CreatePerson(); // bara för att debugga med Runar

        [HttpGet]
        public IActionResult Index()
        {
            /*addPerson.FirstName = "Runar";          // bara för att debugga
            addPerson.LastName = "Bengtsson";
            MyService.Add(addPerson);*/

            return View(MyService.All());           // (Denna innehåller nu en static lista på alla personer)
        }


        [HttpGet]
        public IActionResult Remove(int Id)
        {
            MyService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Create(People theModel)    // Still "" in datafields
        {
            if (ModelState.IsValid)     // Eftersom inte datat kommer in ordentligt fungerar inte IsValid heller.
            {
                MyService.Add(theModel.Person);
                return RedirectToAction(nameof(Index)); //The RedirectToAction() method makes new requests, and URL in the
            }                                           // browser's address bar is updated with the generated URL by MVC.
            return View("Index", theModel);

        }


    }
}
