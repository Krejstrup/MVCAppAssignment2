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
        private readonly PeopleDbContext _myDbContext;  // Dependency Inject here (as #7)

        public DatabasePeopleRepo(PeopleDbContext peopleDbIn)     // Gets the PeopleDbContext without "using"?
        {
            _myDbContext = peopleDbIn;
        }




        // == It's time for the C.R.U.D. implementations : ======

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

            _myDbContext.Peoples.Add(aNewPerson);
            int result = _myDbContext.SaveChanges();
            // SqlException: Cannot insert the value NULL into column 'CityId', table 'PeopleDB.dbo.Peoples';
            // column does not allow nulls. INSERT fails.
            // The statement has been terminated.

            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("Could not write to database");
            }

            return aNewPerson;
        }


        public Person Read(int id)
        {
            return _myDbContext.Peoples.SingleOrDefault(row => row.Id == id);
        }


        public List<Person> Read()
        {
            return _myDbContext.Peoples.ToList();
        }


        public Person Update(Person aPerson)
        {

            Person newPerson = new Person()
            {
                Id = aPerson.Id,
                FirstName = aPerson.FirstName,
                LastName = aPerson.LastName,
                Phone = aPerson.Phone,
                City = aPerson.City
            };


            _myDbContext.Peoples.Update(newPerson);

            int result = _myDbContext.SaveChanges();

            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("No updates was done!");
            }

            return newPerson;

        }


        public bool Delete(Person aPerson)
        {

            _myDbContext.Peoples.Remove(aPerson);

            return _myDbContext.SaveChanges() > 0 ? true : false;

        }
    }
}
