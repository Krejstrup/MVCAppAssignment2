using Microsoft.EntityFrameworkCore;
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



        //--------------Create object in database------------------------

        public Person Create(CreatePerson aPerson)
        {
            Person aNewPerson = new Person()
            {
                //Id = ++_id,                  // The framework database handler takes care of this now
                FirstName = aPerson.FirstName,
                LastName = aPerson.LastName,
                Phone = aPerson.Phone,
                CityId = aPerson.CityId,       // NEW: int CityId from the ViewModel && City InCity from the Controller
                InCity = _myDbContext.Cities.SingleOrDefault(row => row.Id == aPerson.CityId)
            };

            _myDbContext.Peoples.Add(aNewPerson);
            int result = _myDbContext.SaveChanges();


            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("Could not write to database");
            }

            return aNewPerson;
        }


        //-------------Read from database--------------------------------------

        public List<Person> Read()
        {
            return _myDbContext.Peoples.Include(place => place.InCity).ToList();
        }


        public Person Read(int id)
        {
            return _myDbContext.Peoples.Include(place => place.InCity).SingleOrDefault(row => row.Id == id);
        }



        //--------------Update database-----------------------------------------


        public Person Update(Person aPerson)
        {

            Person newPerson = new Person()
            {
                Id = aPerson.Id,
                FirstName = aPerson.FirstName,
                LastName = aPerson.LastName,
                Phone = aPerson.Phone,
                CityId = aPerson.CityId,
                InCity = _myDbContext.Cities.SingleOrDefault(row => row.Id == aPerson.CityId)
            };


            _myDbContext.Peoples.Update(newPerson);

            int result = _myDbContext.SaveChanges();

            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("No updates was done!");
            }

            return newPerson;

        }


        //-----------Delete Database object-------------------------------------------


        public bool Delete(Person aPerson)
        {

            _myDbContext.Peoples.Remove(aPerson);

            return _myDbContext.SaveChanges() > 0 ? true : false;

        }
    }
}
