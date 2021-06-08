using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Repo;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Models.Service
{
    public class CityService : ICityService
    {

        //------------ Dependency Injection -------------------------

        ICityRepo _myCityRepo;

        public CityService(ICityRepo theRepo)
        {
            _myCityRepo = theRepo;
        }



        //------------- Now for some Service -----------------------

        /// <summary>
        /// All fetches all the Cities from the database.
        /// </summary>
        /// <returns>Returns a new View Model object Cities to caller with all the cities.</returns>
        public Cities All()
        {
            Cities theWholeList = new Cities();

            theWholeList.CityList = _myCityRepo.Read();

            return theWholeList;
        }


        /// <summary>
        /// The API version of the All() method clears all the references to all the
        /// people that lives in this City.
        /// </summary>
        /// <returns>The list of Cities stored without the people connected to them.</returns>
        public Cities ApiAll()
        {
            Cities theWholeList = new Cities();

            theWholeList.CityList = _myCityRepo.Read();

            foreach (City theCity in theWholeList.CityList)
            {
                theCity.Peoples = null;
            }

            return theWholeList;
        }


        //-----------Add--------------------------------------

        /// <summary>
        /// Create a new city uses the CreateCity View Model to get the data.
        /// Inserts the new City into the Database and returns it to caller.
        /// </summary>
        /// <param name="aCity"></param>
        /// <returns>Returns the City created and added into the Database</returns>
        public City Add(CreateCity aCity)
        {
            return _myCityRepo.Create(aCity);
        }


        //-----------Edit-------------------------------------

        /// <summary>
        /// Edit is used to change the name of the particular City.
        /// </summary>
        /// <param name="id">The unique id of the original City.</param>
        /// <param name="aCity">This City has the changed data.</param>
        /// <returns>Returns the updated city if successful otherwise null.</returns>
        public City Edit(int id, City aCity)
        {
            City oldCity = FindBy(id);
            if (oldCity == null)
            {
                return null;
            }

            oldCity.Name = aCity.Name;
            oldCity.Country = aCity.Country;
            oldCity.CountryId = aCity.CountryId;
            oldCity.Peoples = aCity.Peoples;

            return _myCityRepo.Update(oldCity);
        }




        //-----------Find------------------------------------

        /// <summary>
        /// FindBy uses the unique id to find the right City.
        /// </summary>
        /// <param name="id">The unique id used to find the right City.</param>
        /// <returns>Returns the City object if found, otherwise null.</returns>
        public City FindBy(int id)
        {
            foreach (City aCity in _myCityRepo.Read())
            {
                if (aCity.Id == id)
                {
                    return aCity;
                }
            }
            return null;
        }

        //---------Remove------------------------------------

        /// <summary>
        /// Remove deletes the City from the Database.
        /// </summary>
        /// <param name="id">THe unique id pointing out the specific City.</param>
        /// <returns>Returns bool true if successful otherwise false.</returns>
        public bool Remove(int id)
        {
            City aCity = FindBy(id);
            if (aCity == null)
            {
                return false;
            }
            return _myCityRepo.Delete(aCity); // This also removes all the childs!!
        }


        //---------Update------------------------------------

        /// <summary>
        /// This method updates the City object in the database.
        /// </summary>
        /// <param name="theCity">The object to update.</param>
        /// <returns>Returns bool true if successful otherwise false.</returns>
        public bool Update(City theCity)
        {
            _myCityRepo.Update(theCity);
            return true;
        }
    }
}
