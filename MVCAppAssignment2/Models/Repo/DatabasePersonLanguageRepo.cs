using MVCAppAssignment2.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCAppAssignment2.Models.Repo
{
    public class DatabasePersonLanguageRepo : IPersonLanguage
    {
        //----Create Dependency injection --------------------------------

        private readonly PeopleDbContext _myDbContext;
        public DatabasePersonLanguageRepo(PeopleDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }




        //========== Implement the CRUD ====================================



        public PersonLanguage Create(PersonLanguage aPersonLanguage)
        {

            _myDbContext.PersonLanguages.Add(aPersonLanguage);

            // If the Savechange is Zero, that means that the update didn't succeed.
            // But otherwise we will return the PersonLanguage object
            int result = _myDbContext.SaveChanges();

            return (result == 0 ? throw new Exception("Could not write to database") : aPersonLanguage);
        }


        //------- Read out the PersonLanguage that are common to both the person and language
        public PersonLanguage Read(int personId, int languageId)
        {
            return _myDbContext.PersonLanguages.SingleOrDefault(row => row.PersonId == personId && row.LanguageId == languageId);
        }




        //------- Fetch a list of all the objects---------------------
        public List<PersonLanguage> Read()
        {
            return _myDbContext.PersonLanguages.ToList();
        }




        //-------- Update are not implemented yet----------------------
        public PersonLanguage Update(PersonLanguage aPersonLanguage)
        {
            throw new NotImplementedException();
        }




        //--------Removes the binding of language and person------------
        public bool Delete(int personId, int languageId)
        {
            // Fetch and test if the object exixts in the database:
            PersonLanguage aPersonLanguage = Read(personId, languageId);
            if (aPersonLanguage == null)
            {
                return false;
            }

            _myDbContext.PersonLanguages.Remove(aPersonLanguage);

            // If the Changes is successful then return true, otherwise false
            return (_myDbContext.SaveChanges() == 0 ? false : true);

        }
    }
}
