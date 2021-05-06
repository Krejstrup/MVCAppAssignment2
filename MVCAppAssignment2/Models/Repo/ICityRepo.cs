using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System.Collections.Generic;

namespace MVCAppAssignment2.Models.Repo
{
    public interface ICityRepo
    {

        // Let's have some new C.R.U.D.:

        City Create(CreateCity aCity);

        List<City> Read();

        City Read(int id);

        City Update(City aCity);

        bool Delete(City aCity);
    }
}
