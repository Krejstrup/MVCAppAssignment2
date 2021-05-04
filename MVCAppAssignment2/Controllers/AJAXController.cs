﻿using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;



namespace MVCAppAssignment2.Controllers
{

    public class AJAXController : Controller
    {
        private readonly IPeopleService _myService;

        public AJAXController(IPeopleService theService)//constuctor injection
        {
            _myService = theService;
        }

        /// <summary>
        /// Index loads the entire page with a view.
        /// </summary>
        /// <returns>The standard view to display.</returns>
        [HttpGet]
        public IActionResult Index()       // WORKS!!
        {
            return View();
        }


        /// <summary>
        /// AllPersons returns the list of all persons in the database/memory.
        /// </summary>
        /// <returns>The list of all the persons in the database/memory.</returns>
        [HttpGet]
        public IActionResult AllPersons() // WORKS!!
        {
            // Send down all the persons in this Model
            People everyPerson = _myService.All();
            return PartialView("_AllPersonsPartial", everyPerson);
        }


        /// <summary>
        /// Filter is only using the person Id to filter out one person.
        /// </summary>
        /// <param name="Id">The person to look up.</param>
        /// <returns>Returns a partial view of a person on success, otherwise NotFound (404).</returns>
        [HttpPost]
        public IActionResult Filter(int Id)
        {
            Person onePerson = _myService.FindBy(Id);

            if (onePerson != null)
            {
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
        public IActionResult About(int Id) // WORKS!!
        {
            Person onePerson = _myService.FindBy(Id);

            if (onePerson != null)
            {
                return PartialView("_PersonAboutPartial", onePerson);
            }
            return PartialView("_AllPersonsPartial", _myService.All());
        }

        /// <summary>
        /// NotAbout closes the info partial view for the person clicked. The person partial view is replaced.
        /// </summary>
        /// <param name="Id">The Id pointing out the person view to replace About with.</param>
        /// <returns>Returns the standard partial view of a person.</returns>
        [HttpPost]
        public IActionResult NotAbout(int Id) // WORKS!!
        {
            Person onePerson = _myService.FindBy(Id);

            if (onePerson != null)
            {
                return PartialView("_PersonPartial", onePerson);
            }
            return PartialView("_AllPersonsPartial", _myService.All());
        }


        /// <summary>
        /// Remove deletes the given person from the database/memory. And deletes the representing personal row.
        /// </summary>
        /// <param name="Id">The Id of the person to be removed.</param>
        /// <returns>Returns Ok (200) if success otherwise BadRequest or NotFound (404)</returns>
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
        }



        /// <summary>
        /// Edit is not quite finished yet!!!
        /// </summary>
        /// <param name="aPerson">The Person to be edited.</param>
        /// <returns>Returns Ok (200) and data for the person Id on success, otherwise BadRequest or NotFound (404)</returns>
        [HttpPost]
        public IActionResult Edit(Person aPerson) // WORKS NOT QUITE YET!!
        {

            if (aPerson == null)
            {
                return NotFound();
            }

            if (_myService.Edit(aPerson.Id, aPerson) != null)
            {
                return Ok(aPerson.Id);
            }

            return BadRequest();
        }
    }
}
