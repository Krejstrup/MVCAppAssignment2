using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Models.Service
{
    /// <summary>
    /// People service is an implementation of the interface IPeopleService.
    /// Purpose: To act as a handler for any type of memory data handler...
    /// </summary>
    public class PeopleService : IPeopleService
    {

        private static InMemoryPeopleRepo memoryList = new InMemoryPeopleRepo();




        /// <summary>
        /// Add-function appends a person to the list. The Id is handled by the Repo.
        /// </summary>
        /// <param name="addPerson">This data param comes from the Create View Form.</param>
        /// <returns>Returns the newly created Person.</returns>
        public Person Add(CreatePerson addPerson)
        {
            return memoryList.Create(addPerson.FirstName, addPerson.LastName, addPerson.Phone, addPerson.City);
        }


        /// <summary>
        /// All() returns the entire list of all Persons stored in memory in a People-ViewModel.
        /// The People-ViewModel is entirely just a List of People.
        /// </summary>
        /// <returns>Returns all people in the memory.</returns>
        public People All()
        {
            People theWholeList = new People();

            theWholeList.PersonList = memoryList.Read();
            return theWholeList;
        }


        /// <summary>
        /// FindBy(search) uses all params in a Person data model from a Form, or otherwise,
        /// to lookup all persons in memory to match the search. All matches is put into a
        /// new list.
        /// </summary>
        /// <param name="search">A person data template with search data.</param>
        /// <returns>Returns a new list of all the matching searches. If non found the list is empty.</returns>
        public People FindBy(People search)
        {
            People newDataModel = new People();
            string lookup = search.filter;

            if (lookup != null || lookup != "")          // looking for string match or Person data match??
            {
                foreach (Person memPers in memoryList.Read())
                {
                    if (lookup == memPers.FirstName)
                    {
                        newDataModel.PersonList.Add(memPers);
                    }
                    else if (lookup == memPers.LastName)
                    {
                        newDataModel.PersonList.Add(memPers);
                    }
                    else if (lookup == memPers.Phone)
                    {
                        newDataModel.PersonList.Add(memPers);
                    }
                    else if (lookup == memPers.City)
                    {
                        newDataModel.PersonList.Add(memPers);
                    }

                }


            }
            else if (search.PersonList.Count > 0) // Lookong for a Person data match from form input
            {
                foreach (Person memPers in memoryList.Read())
                {
                    foreach (Person searchPers in search.PersonList)
                    {
                        if (searchPers.FirstName == memPers.FirstName)
                        {
                            newDataModel.PersonList.Add(memPers);
                        }
                        else if (searchPers.LastName == memPers.LastName)
                        {
                            newDataModel.PersonList.Add(memPers);
                        }
                        else if (searchPers.Phone == memPers.Phone)
                        {
                            newDataModel.PersonList.Add(memPers);
                        }
                        else if (searchPers.City == memPers.City)
                        {
                            newDataModel.PersonList.Add(memPers);
                        }
                    }

                }
            }
            return newDataModel;
        }


        /// <summary>
        /// FindBy (id) has only one search option - the Id.
        /// </summary>
        /// <param name="id">The unique Id of a person that should be looked for.</param>
        /// <returns>Retuns the found person or null if person not found.</returns>
        public Person FindBy(int id)
        {
            foreach (Person aPerson in memoryList.Read())
            {
                if (aPerson.Id == id)
                {
                    return aPerson;
                }
            }
            return null;
        }


        /// <summary>
        /// Edit (id Person) uses the id to lookup the right person and then
        /// updates the data from the data template.
        /// The data is not checked so empty fields will overwrite data in person!
        /// </summary>
        /// <param name="id">The unique id for the person to lookup.</param>
        /// <param name="person">The data that the person should be updated with.</param>
        /// <returns>Returns the person or null if not found.</returns>
        public Person Edit(int id, Person person)
        {
            Person aPerson = FindBy(id);
            if (aPerson != null)
            {
                aPerson.FirstName = person.FirstName;
                aPerson.LastName = person.LastName;
                aPerson.Phone = person.Phone;
                aPerson.City = person.City;
            }

            return aPerson;
        }

        /// <summary>
        /// Remove will delete the person maching the id, sent in as parameter, from the memory list.
        /// </summary>
        /// <param name="id">The unique id of the person to remove.</param>
        /// <returns>Returns true if sucsess, false otherwise.</returns>
        public bool Remove(int id)
        {
            Person aPerson = FindBy(id);
            return memoryList.Delete(aPerson);
        }
    }
}
