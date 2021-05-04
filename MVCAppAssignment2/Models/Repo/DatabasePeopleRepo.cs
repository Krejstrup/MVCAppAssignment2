using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCAppAssignment2.Models.Repo
{
    public class DatabasePeopleRepo : IPeopleRepo
    {
        //private static int _id = 0;                       // The framework database handler takes care of Id/Key now
        private readonly PeopleDbContext _peopleDbContext;  // Dependency Inject here (as #7)

        public DatabasePeopleRepo(PeopleDbContext peopleDbIn)     // Gets the PeopleDbContext without "using"?
        {
            _peopleDbContext = peopleDbIn;
        }




        public Person Create(CreatePerson aPerson)
        {
            Person aNewPerson = new Person()
            {
                //Id = ++_id,                  // The framework database handler takes care of this now
                FirstName = aPerson.FirstName,
                LastName = aPerson.LastName,
                Phone = aPerson.Phone,
                City = aPerson.City
            };

            _peopleDbContext.Peoples.Add(aNewPerson);
            int result = _peopleDbContext.SaveChanges();

            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("Could not write to database");
            }

            return aNewPerson;
        }


        public bool Delete(Person aPerson)
        {
            throw new NotImplementedException();
        }

        public Person Read(int id)
        {
            return _peopleDbContext.Peoples.SingleOrDefault(row => row.Id == id);
        }


        public List<Person> Read()
        {
            return _peopleDbContext.Peoples.ToList();
        }


        public Person Update(Person aPerson)
        {
            throw new NotImplementedException();
        }
    }
}
