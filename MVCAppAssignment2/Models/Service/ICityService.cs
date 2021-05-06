using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Models.Service
{
    public interface ICityService
    {

        public City Add(CreateCity aCity);

        //public City Add(CreateCity aCity, int aCountryId);

        public Cities All();

        //public Cities FindBy(City search);

        public City FindBy(int id);

        public City Edit(int id, City aCity);

        public bool Remove(int id);
    }
}
