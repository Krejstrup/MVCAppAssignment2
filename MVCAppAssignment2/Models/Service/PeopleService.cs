using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Repo;
using MVCAppAssignment2.Models.ViewModel;
using System;
using System.Collections.Generic;

namespace MVCAppAssignment2.Models.Service
{
    /// <summary>
    /// People service is an implementation of the interface IPeopleService.
    /// Purpose: To act as a handler for any type of memory data handler...
    /// Dependency Injection using the constructor.
    /// </summary>
    public class PeopleService : IPeopleService
    {

        IPeopleRepo _myPeopleDbRepo;
        IPersonLanguage _myPersonLanguageDbRepo;


        public PeopleService(IPeopleRepo theRepo, IPersonLanguage PersonLanguageDbRepo)
        {
            _myPeopleDbRepo = theRepo;
            _myPersonLanguageDbRepo = PersonLanguageDbRepo;
        }




        /// <summary>
        /// Add-function appends a person to the list. The Id is handled by the Repo.
        /// </summary>
        /// <param name="addPerson">This data param comes from the Create View Form.</param>
        /// <returns>Returns the newly created Person.</returns>
        public Person Add(CreatePerson addPerson)
        {
            return _myPeopleDbRepo.Create(addPerson);
        }


        public PersonLanguage AddLanguageToPerson(int perId, int langId)
        {
            PersonLanguage theBinding = new PersonLanguage()
            {
                PersonId = perId,
                LanguageId = langId
            };

            PersonLanguage thePersonLanguage = _myPersonLanguageDbRepo.Create(theBinding);

            return thePersonLanguage;
        }


        public bool RemoveLanguageFromPerson(int perId, int langId)
        {
            return _myPersonLanguageDbRepo.Delete(perId, langId);
        }



        /// <summary>
        /// All() returns the entire list of all Persons stored in memory in a People-ViewModel.
        /// The People-ViewModel is entirely just a List of People.
        /// </summary>
        /// <returns>Returns all people in the memory.</returns>
        public People All()
        {
            People theWholeList = new People();

            theWholeList.PersonList = _myPeopleDbRepo.Read();
            return theWholeList;
        }

        /// <summary>
        /// ApiAll returns the entire list of all persons stored in memory. The list contains
        /// lists of Cities and Languages that the forward object to furter objects is emptied.
        /// </summary>
        /// <returns>Returns the entaire list </returns>
        public People ApiAll()
        {
            People theWholeList = new People();

            theWholeList.PersonList = _myPeopleDbRepo.Read();
            foreach (Person item in theWholeList.PersonList)      // TODO: make a new function in the Service for this!!!
            {
                if (item.InCity != null)
                {
                    item.InCity.Peoples = null;

                    if (item.InCity.Country != null)
                    {
                        item.InCity.Country.Cities = null;
                    }
                }

                foreach (PersonLanguage language in item.PersonLanguages)
                {
                    language.Person = null;
                }
            }
            return theWholeList;
        }


        /// <summary>
        /// FindBy(search) uses all params in a Person data model from a Form, or otherwise,
        /// to lookup all persons in memory to match the search. All matches is put into a
        /// new list.
        /// </summary>
        /// <param name="search">A person data template with string search, from the filter data.</param>
        /// <returns>Returns a new list of all the matching searches. If non found the list is empty.</returns>
        public People FindBy(People search)
        {
            People newDataModel = new People();
            string lookup = search.filter;

            if (newDataModel.PersonList == null)
            {
                newDataModel.PersonList = new List<Person>();
            }

            if (lookup != null && lookup != "")          // Shall we start looking string match by filter input?
            {
                int parseInt = 0;
                bool isNumber = false;
                foreach (Person memPers in _myPeopleDbRepo.Read())
                {
                    isNumber = Int32.TryParse(lookup, out parseInt);
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
                    else if (isNumber)
                    {
                        if (parseInt == memPers.CityId)
                            newDataModel.PersonList.Add(memPers);
                    }
                }
            }
            else if (search.PersonList.Count > 0)       // Proceed looking for a Person matching a form-input:
            {
                foreach (Person memPers in _myPeopleDbRepo.Read())
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
                        else if (searchPers.CityId == memPers.CityId)
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
            foreach (Person aPerson in _myPeopleDbRepo.Read())
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
                return null;
            }

            aPerson.FirstName = person.FirstName;
            aPerson.LastName = person.LastName;
            aPerson.Phone = person.Phone;
            aPerson.CityId = person.CityId;
            aPerson.InCity = person.InCity;
            aPerson.PersonLanguages = person.PersonLanguages;

            return _myPeopleDbRepo.Update(aPerson);
        }

        /// <summary>
        /// Edit (id Person) uses the id to lookup the right person and then
        /// updates the data from the data template.
        /// The data is not checked so empty fields will overwrite data in person!
        /// </summary>
        /// <param name="id">The unique id for the person to lookup.</param>
        /// <param name="person">The data that the person should be updated with.</param>
        /// <returns>Returns the person or null if not found.</returns>
        public Person Edit(int id, EditPerson person)
        {
            Person aPerson = FindBy(id);
            if (aPerson != null)
            {
                return null;
            }

            aPerson = person.Person;

            return _myPeopleDbRepo.Update(aPerson);
        }





        /// <summary>
        /// Remove will delete the person maching the id, sent in as parameter, from the memory list.
        /// </summary>
        /// <param name="id">The unique id of the person to remove.</param>
        /// <returns>Returns true if sucsess, false otherwise.</returns>
        public bool Remove(int id)
        {
            Person aPerson = FindBy(id);
            if (aPerson == null)
            {
                return false;
            }
            return _myPeopleDbRepo.Delete(aPerson);
        }



        /// <summary>
        /// This is an overload for the RemoveBy Id. The former looks up the person, in this overload
        /// the person is allready known.
        /// </summary>
        /// <param name="aPerson">A person known from a previous lookup.</param>
        /// <returns></returns>
        public bool Remove(Person aPerson)
        {
            if (aPerson != null)
            {
                return _myPeopleDbRepo.Delete(aPerson);
            }
            else
            {
                return false;
            }

        }

    }
}
