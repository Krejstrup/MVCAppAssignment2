using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Repo;
using MVCAppAssignment2.Models.ViewModel;
using System;

namespace MVCAppAssignment2.Models.Service
{
    public class CityService : ICityService
    {

        ICityRepo _myCityRepo;

        public CityService(ICityRepo theRepo)
        {
            _myCityRepo = theRepo;
        }



        public Cities All()
        {
            Cities theWholeList = new Cities();

            theWholeList.CityList = _myCityRepo.Read();

            return theWholeList;
        }

        public City Add(CreateCity aCity)
        {
            return _myCityRepo.Create(aCity);
        }

        public City Edit(int id, City aCity)
        {
            City newCity = FindBy(id);
            if (newCity != null)
            {
                newCity.Name = aCity.Name;
            }

            return newCity;
        }

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

        public bool Remove(int id)
        {
            City aCity = FindBy(id);
            if (aCity == null)
            {
                return false;
            }
            return _myCityRepo.Delete(aCity);
        }
    }
}
