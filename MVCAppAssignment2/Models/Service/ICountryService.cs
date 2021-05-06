using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Models.Service
{
    public interface ICountryService
    {
        public Country Add(CreateCountry aCountry);

        public Countries All();

        //public Countries FindBy(Country search);

        public Country FindBy(int id);

        public Country Edit(int id, Country aCountry);

        public bool Remove(int id);

    }
}
