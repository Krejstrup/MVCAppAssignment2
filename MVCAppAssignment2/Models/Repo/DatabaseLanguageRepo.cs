using Microsoft.EntityFrameworkCore;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCAppAssignment2.Models.Repo
{
    public class DatabaseLanguageRepo : ILanguageRepo
    {

        private readonly PeopleDbContext _myDbContext;      // Dependency Inject here (as #7)
        public DatabaseLanguageRepo(PeopleDbContext LanguageDbIn)
        {
            _myDbContext = LanguageDbIn;
        }



        //-------- It's time for the C.R.U.D. implementations: --------



        public Language Create(CreateLanguage aLanguage)
        {
            Language newLanguage = new Language
            {
                Name = aLanguage.Name
            };

            _myDbContext.Languages.Add(newLanguage);
            int result = _myDbContext.SaveChanges();

            if (result == 0) throw new Exception("Could not write to database");

            return newLanguage;
        }


        //-------------  Read ----------------------------------------
        public List<Language> Read()
        {
            return _myDbContext.Languages.Include(row => row.PersonLanguages).ToList();
        }

        public Language Read(int id)
        {
            return _myDbContext.Languages.Include(row => row.PersonLanguages).SingleOrDefault(row => row.Id == id);
        }

        public Language Read(int pId, int lId)
        {
            // This is a double lookup-> both the person and the language id's has to match! Lambda with double condition
            return _myDbContext.Languages.SingleOrDefault(lang => lang.Id == pId && lang.Id == lId);
        }


        //------------- Update ----------------------------------------
        public Language Update(Language aLanguage)
        {
            throw new NotImplementedException();
        }




        //------------ Delete a Language ------------------------------
        public bool Delete(Language aLanguage)
        {
            int id = aLanguage.Id;
            Language thisLanguage = _myDbContext.Languages.Find(id);

            _myDbContext.Languages.Remove(thisLanguage);

            return (_myDbContext.SaveChanges() == 0) ? false : true;

        }

        public bool Delete(int personId, int langId)
        {
            Language thisLanguage = _myDbContext.Languages.Find(personId, langId);

            _myDbContext.Languages.Remove(thisLanguage);


            return (_myDbContext.SaveChanges() == 0) ? false : true;

        }
    }
}
