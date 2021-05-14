using MVCAppAssignment2.Models.Data;
using System.Collections.Generic;

namespace MVCAppAssignment2.Models.Repo
{
    public interface IPersonLanguage
    {
        //---Make an interface to the CRUD-------------------------

        PersonLanguage Create(PersonLanguage aPersonLanguage);  // Create the "conection"

        PersonLanguage Read(int personId, int languageId);  // Read out the Language/Person related data

        List<PersonLanguage> Read();                        // Read out the list of the 

        PersonLanguage Update(PersonLanguage aPersonLanguage);  // Update the database with this

        bool Delete(int personId, int languageId);        // Remove the language/person connection

    }
}
