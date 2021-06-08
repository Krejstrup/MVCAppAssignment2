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
            List<Person> newPerson = _myDbContext.Peoples
                                        .Include(row => row.InCity)
                                        .Include(row => row.PersonLanguages)
                                        .ToList();
            return newPerson;
        }


        public Person Read(int id)
        {
            return _myDbContext.Peoples
                .Include(place => place.InCity)
                .Include(lan => lan.PersonLanguages)
                    .ThenInclude(lanN => lanN.Language)
                .SingleOrDefault(row => row.Id == id);
        }


        //--------------Edit and update object in database-----------------------------------------




        //--------------Update database-----------------------------------------


        public Person Update(Person aPerson)
        {
            Person newPerson = Read(aPerson.Id); //new Person()

            if (newPerson == null)
            {
                return null;
            }


            _myDbContext.Peoples.Update(aPerson);

            int result = _myDbContext.SaveChanges();

            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("No updates was done!");
            }

            return aPerson;

        }


        //-----------Delete Database object-------------------------------------------


        public bool Delete(Person aPerson)
        {
            _myDbContext.Peoples.Remove(aPerson);

            return _myDbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
