using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Repo;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Models.Service
{
    public class CountryService : ICountryService
    {

        ICountryRepo _myCountryRepo;

        public CountryService(ICountryRepo theRepo)
        {
            _myCountryRepo = theRepo;
        }

        //---------Now some database work-----------------



        public Country Add(CreateCountry aCountry)
        {
            return _myCountryRepo.Create(aCountry);
        }



        public Countries All()
        {
            Countries theWholeList = new Countries();

            theWholeList.CountryList = _myCountryRepo.Read();

            return theWholeList;
        }


        //-------------Edit--------------------------------
        public Country Edit(int id, Country country)
        {
            Country aCountry = FindBy(id);
            if (aCountry == null)
            {
                return null;
            }

            aCountry.Name = country.Name;
            aCountry.Cities = country.Cities;
            _myCountryRepo.Update(aCountry);    // Just send for update, with a new name and List-update!

            return aCountry;
        }


        //-------------Find--------------------------------
        public Countries FindBy(Country search) //??? Finns det verkligen flera länder med samma namn?? :D 
        {
            return null;
        }



        public Country FindBy(int id)
        {
            foreach (Country aCountry in _myCountryRepo.Read())
            {
                if (aCountry.Id == id)
                {
                    return aCountry;
                }
            }
            return null;
        }



        //-------------Remove-------------------------------
        public bool Remove(int id)
        {
            Country aCountry = FindBy(id);
            if (aCountry == null)
            {
                return false;
            }
            return _myCountryRepo.Delete(aCountry);
        }

    }
}
