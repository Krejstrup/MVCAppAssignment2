using Microsoft.EntityFrameworkCore;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCAppAssignment2.Models.Repo
{
    public class DatabaseCountryRepo : ICountryRepo
    {


        private readonly PeopleDbContext _myDbContext;  // Dependency Inject here (as #7)

        public DatabaseCountryRepo(PeopleDbContext countryDbIn)
        {
            _myDbContext = countryDbIn;
        }


        // == It's time for the C.R.U.D. implementations : ======


        /// <summary>
        /// Create uses a CreateCountry data class to get the data for a new country.
        /// </summary>
        /// <param name="aCountry">The data object to be used to create a new country.</param>
        /// <returns>Returns the new created country.</returns>
        public Country Create(CreateCountry aCountry)
        {

            Country aNewCountry = new Country()
            {
                Name = aCountry.Name
            };

            _myDbContext.Countries.Add(aNewCountry);

            int result = _myDbContext.SaveChanges();

            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("Could not write to database");
            }

            return aNewCountry;

        }


        /// <summary>
        /// Read will look up the country that matches the id and return the object.
        /// </summary>
        /// <param name="id">The unique Id for the Country.</param>
        /// <returns>The country that matches with the id, otherwise null.</returns>
        public Country Read(int id)
        {
            return _myDbContext.Countries.SingleOrDefault(row => row.Id == id);
        }



        /// <summary>
        /// Read fetches all the countries in the database.
        /// </summary>
        /// <returns>Return a list of all the countries including the cities assossiated to given country.</returns>
        public List<Country> Read()
        {
            return _myDbContext.Countries.Include(row => row.Cities).ToList(); // Includes all the Cities, From Lazy loading to 
        }





        /// <summary>
        /// Update takes a complete Country and updates the data to the database.
        /// </summary>
        /// <param name="aCountry">The data contained in the Country will be transfered to the Database.</param>
        /// <returns>Returns the full copy of the updated data.</returns>
        public Country Update(Country aCountry)
        {
            Country newCountry = new Country()
            {
                Id = aCountry.Id,
                Name = aCountry.Name
            };

            _myDbContext.Countries.Update(newCountry);

            int result = _myDbContext.SaveChanges();

            if (result == 0)    // 0 is no changes to database
            {
                throw new Exception("No updates was done!");
            }

            return newCountry;
        }




        /// <summary>
        /// Detete will remove the Country from the database. The Cities assosiated to this country will not be effected.
        /// </summary>
        /// <param name="aCountry"></param>
        /// <returns>Return true if the removal worked, otherwise false.</returns>
        public bool Delete(Country aCountry)
        {
            _myDbContext.Countries.Remove(aCountry);

            return _myDbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
