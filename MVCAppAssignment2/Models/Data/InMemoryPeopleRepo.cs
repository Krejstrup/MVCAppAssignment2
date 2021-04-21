using System.Collections.Generic;

namespace MVCAppAssignment2.Models.Data
{
    public class InMemoryPeopleRepo : IPeopleRepo
    {
        private static int _id = 0;
        private static List<Person> _personList = new List<Person>();


        /// <summary>
        /// Create adds a new person to the memory list of persons. Everyone gets an unique id.
        /// </summary>
        /// <param name="inFirstName">The name of this persons first name.</param>
        /// <param name="inLastName">The name of this persons family name.</param>
        /// <param name="phone">The phone number of this person.</param>
        /// <param name="city">The city of this person</param>
        /// <returns>Returns the new person object.</returns>
        public Person Create(string inFirstName, string inLastName, string phone, string city)
        {
            Person aNewPerson = new Person(++_id, inFirstName, inLastName, phone, city);

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
                    mate.City = person.City;

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
