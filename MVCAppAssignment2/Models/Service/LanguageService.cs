using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Repo;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Models.Service
{

    /// <summary>
    /// Class language, handeling service provided for the Controller.
    /// </summary>
    public class LanguageService : ILanguageService
    {

        // Add the Repo to the service
        ILanguageRepo _myLangRepo;

        /// <summary>
        /// The data handeling service provided for the Controller.
        /// </summary>
        /// <param name="languageRepo">The repo object.</param>
        public LanguageService(ILanguageRepo languageRepo)  // Dependency Inject here (as #7)
        {
            _myLangRepo = languageRepo;

        }



        //-------------Dr.Snuggles will now do some science tricks-----------------

        /// <summary>
        /// Add appends a new languge to the database using View Model CreateLanguage.
        /// </summary>
        /// <param name="aLanguage">The language that that should be added.</param>
        /// <returns>Returns the new language if successful, otherwise null.</returns>
        public Language Add(CreateLanguage aLanguage)
        {
            return _myLangRepo.Create(aLanguage);
        }


        /// <summary>
        /// Gets the entire list of language from the database.
        /// </summary>
        /// <returns>All the languages stored in the database.</returns>
        public Languages All()
        {
            Languages aNewLanguagesSet = new Languages();

            aNewLanguagesSet.LanguageList = _myLangRepo.Read();

            return aNewLanguagesSet;
        }


        /// <summary>
        /// Gets the entire list of language from the database. And erases all the
        /// referenses to the Persons that uses this Language.
        /// </summary>
        /// <returns>All the languages stored in the database.</returns>
        public Languages ApiAll()
        {
            Languages aNewLanguagesSet = new Languages();

            aNewLanguagesSet.LanguageList = _myLangRepo.Read();

            foreach (Language lang in aNewLanguagesSet.LanguageList)
            {
                lang.PersonLanguages = null;
            }

            return aNewLanguagesSet;
        }


        /// <summary>
        /// Edit lang updates the name of the language pointed out by id with the
        /// data from the parameter aLanguage.
        /// </summary>
        /// <param name="id">The unique id of the language.</param>
        /// <param name="aLanguage">The object containing tha data.</param>
        /// <returns></returns>
        public Language Edit(int id, Language aLanguage)
        {
            Language orgLang = FindBy(id);

            orgLang.Name = aLanguage.Name;

            return _myLangRepo.Update(orgLang);
        }

        /// <summary>
        /// FindBy uses the unique id to find and return the right object.
        /// </summary>
        /// <param name="id">The unique id for the object.</param>
        /// <returns>Returns the object if successfull otherwise null.</returns>
        public Language FindBy(int id)
        {
            return _myLangRepo.Read(id);
        }


        /// <summary>
        /// Remove deletes the object in the database.
        /// </summary>
        /// <param name="id">The unique id for the object to remove.</param>
        /// <returns>Returns bool true if successfunl, otherwise false.</returns>
        public bool Remove(int id)
        {
            Language delLang = FindBy(id);
            return _myLangRepo.Delete(delLang);
        }
    }
}
