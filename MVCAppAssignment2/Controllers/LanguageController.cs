using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Service;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Controllers
{
    public class LanguageController : Controller
    {

        private readonly ILanguageService _myLangService;
        private readonly IPeopleService _myPeopleService;
        public LanguageController(ILanguageService LangService, IPeopleService PeopService)  // Constuctor Dependency Injection
        {
            _myLangService = LangService;
            _myPeopleService = PeopService;
        }




        [HttpGet]
        public IActionResult Index()
        {
            Languages newLang = new Languages()
            {
                LanguageList = _myLangService.All().LanguageList,
                PersonList = _myPeopleService.All().PersonList
            };
            return View(newLang);
        }


        // GET: LanguageController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }




        //---------Create-----------------------------------------------------

        // POST: LanguageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Language inLanguage)
        {
            if (ModelState.IsValid)
            {
                CreateLanguage newLanguage = new CreateLanguage()
                {
                    Name = inLanguage.Name
                };

                _myLangService.Add(newLanguage);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Index", _myLangService.All());
            }
        }


        //-----------Edit------------------------------------------------------

        // GET: LanguageController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: LanguageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
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


        //-----------Delete-----------------------------------------------------------
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _myLangService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        //--------Should we be able to remove a parson from this view??----------------
        public IActionResult PersonDel(Person thePerson)
        {
            // Remove one of the persons to this language here?? 
            //_myLangService
            return View();
        }



    }
}
