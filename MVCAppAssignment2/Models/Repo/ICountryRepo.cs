using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System.Collections.Generic;

namespace MVCAppAssignment2.Models.Repo
{
    public interface ICountryRepo
    {
        // Let's have some new C.R.U.D.:

        Country Create(CreateCountry aCountry);

        List<Country> Read();

        Country Read(int id);

        Country Update(Country aCountry);

        bool Delete(Country aCountry);
    }
}
