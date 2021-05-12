using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Repo;
using MVCAppAssignment2.Models.ViewModel;
using System;

namespace MVCAppAssignment2.Models.Service
{
    public class LanguageService : ILanguageService
    {

        // Add the Repo to the service
        ILanguageRepo _myLangRepo;

        public LanguageService(ILanguageRepo languageRepo)  // Dependency Inject here (as #7)
        {
            _myLangRepo = languageRepo;

        }



        //-------------Dr.Snuggles will now do some science tricks-----------------

        public Language Add(CreateLanguage aLanguage)
        {
            return _myLangRepo.Create(aLanguage);
        }



        public Languages All()
        {
            Languages aNewLanguagesSet = new Languages();

            aNewLanguagesSet.LanguageList = _myLangRepo.Read();

            return aNewLanguagesSet;
        }




        public Language Edit(int id, Language aLanguage)
        {
            throw new NotImplementedException();
        }


        public Language FindBy(int id)
        {
            return _myLangRepo.Read(id);
        }


        public bool Remove(int id)
        {
            Language delLang = FindBy(id);
            return _myLangRepo.Delete(delLang);
        }
    }
}
