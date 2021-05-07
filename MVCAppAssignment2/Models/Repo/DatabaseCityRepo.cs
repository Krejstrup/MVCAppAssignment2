using Microsoft.EntityFrameworkCore;    // to use the Include() keyword for Eager Loading
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCAppAssignment2.Models.Repo
{
    public class DatabaseCityRepo : ICityRepo
    {



        private readonly PeopleDbContext _myDbContext;      // Dependency Inject here (as #7)

        public DatabaseCityRepo(PeopleDbContext cityDbIn)
        {
            _myDbContext = cityDbIn;
        }

        //-------- It's time for the C.R.U.D. implementations :


        // ------------ Create city -----------------------------


        public City Create(CreateCity aCity)
        {
            City aNewCity = new City()
            {
                Name = aCity.Name,
                CountryId = aCity.CountryId
            };

            _myDbContext.Cities.Add(aNewCity);

            int result = _myDbContext.SaveChanges();

            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("Could not write to database");
            }

            return aNewCity;
        }
        //Microsoft.EntityFrameworkCore.DbUpdateException: 'An error occurred while updating the entries. See the inner exception for details.'



        // --------------- Read data --------------------------------

        public City Read(int id)
        {
            return _myDbContext.Cities.Include(country => country.Country).SingleOrDefault(row => row.Id == id);
        }

        public List<City> Read()
        {
            //return _myDbContext.Cities.Include(row => row.CountryId).ToList();
            return _myDbContext.Cities.ToList();
        }


        //----------------- Update data -----------------------------

        public City Update(City aCity)
        {
            City newCity = new City()
            {
                Id = aCity.Id,
                Name = aCity.Name
                //CountryId = aCity.CountryId
            };


            _myDbContext.Cities.Update(newCity);

            int result = _myDbContext.SaveChanges();

            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("No updates was done!");
            }

            return newCity;
        }



        //------------------ Delete City ---------------------------

        public bool Delete(City aCity)
        {
            _myDbContext.Cities.Remove(aCity);

            return _myDbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
