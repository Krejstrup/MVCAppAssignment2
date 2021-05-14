using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Repo;
using MVCAppAssignment2.Models.ViewModel;
using System;

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

        public Cities All()
        {
            Cities theWholeList = new Cities();

            theWholeList.CityList = _myCityRepo.Read();

            return theWholeList;
        }

        //-----------Add--------------------------------------

        public City Add(CreateCity aCity)
        {
            return _myCityRepo.Create(aCity);
        }


        //-----------Edit-------------------------------------

        public City Edit(int id, City aCity)
        {
            City newCity = FindBy(id);
            if (newCity == null)
            {
                return null;
            }

            newCity.Name = aCity.Name;
            newCity.Country = aCity.Country;
            newCity.CountryId = aCity.CountryId;
            newCity.Peoples = aCity.Peoples;

            return _myCityRepo.Update(newCity);
        }




        //-----------Find------------------------------------
        public City FindBy(City search) //??
        {
            throw new NotImplementedException();
        }



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
        public bool Update(City theCity)
        {
            _myCityRepo.Update(theCity);
            return true;
        }
    }
}
