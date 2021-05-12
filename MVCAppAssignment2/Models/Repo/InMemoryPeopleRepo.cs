using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System.Collections.Generic;

namespace MVCAppAssignment2.Models.Repo
{
    public class InMemoryPeopleRepo : IPeopleRepo
    {
        int _id = 0;
        readonly List<Person> _personList;       // This is now a Singleton service from Startup.cs


        public InMemoryPeopleRepo()
        {
            _personList = new List<Person>();
        }


        /// <summary>
        /// Create adds a new person to the memory list of persons. Everyone gets an unique id.
        /// </summary>
        /// <param name="inFirstName">The name of this persons first name.</param>
        /// <param name="inLastName">The name of this persons family name.</param>
        /// <param name="phone">The phone number of this person.</param>
        /// <param name="city">The city of this person</param>
        /// <returns>Returns the new person object.</returns>
        public Person Create(CreatePerson aPerson)  //string inFirstName, string inLastName, string phone, string city
        {   // Could use a Person Data field instead: Create(CreatePerson aPerson) 
            // = new CreatePerson() {Id = ++_id, FirstName = aPerson.FirstName, LastName= aPerson.LastName etc
            Person aNewPerson = new Person()
            {
                Id = ++_id,
                FirstName = aPerson.FirstName,
                LastName = aPerson.LastName,
                Phone = aPerson.Phone,
                //City = aPerson.City   // It's not a string anymore!!!
            };

            _personList.Add(aNewPerson);
            return aNewPerson;
        }


        /// <summary>
        /// Read returns the whole list of persons in the memory.
        /// </summary>
        /// <returns>Returns a List<> of all the Persons.</returns>
        public List<Person> Read()
        {
            return _personList;
        }


        /// <summary>
        /// Read (id) looks up the person matching the unique id and returns the person object.
        /// </summary>
        /// <param name="id">The unique Id for the person.</param>
        /// <returns>Returns the found person by Id, null otherwise.</returns>
        public Person Read(int id)
        {
            foreach (Person mate in _personList)
            {
                if (mate.Id == id)
                {
                    return mate;
                }
            }
            return null;
        }


        /// <summary>
        /// Update uses the person Id to find the right person. Then updates the fields
        /// with the data fields submitted by the person data template. The fields is
        /// not checked for any fails or inconcistencies.
        /// </summary>
        /// <param name="person">The person that shuld be updated.</param>
        /// <returns>Returns the updated person. Returns null if person not found by id.</returns>
        public Person Update(Person person)
        {
            int lookupId = person.Id;
            Person mate = Read(lookupId);

            if (mate != null)
            {
                mate.FirstName = person.FirstName;
                mate.LastName = person.LastName;
                mate.Phone = person.Phone;
                // mate.City = person.City; // It's not a string anymore!!

                return mate;
            }

            return null;
        }


        /// <summary>
        /// Delete removes the person in the memory list.
        /// </summary>
        /// <param name="Person">The person that chould be removed.</param>
        /// <returns>Returns true if deletion worked, false otherwise.</returns>
        public bool Delete(Person Person)
        {
            return _personList.Remove(Person);
        }
    }
}
