using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System.Collections.Generic;

namespace MVCAppAssignment2.Models.Repo
{
    public interface IPeopleRepo
    {
        Person Create(CreatePerson aPerson);

        List<Person> Read();

        Person Read(int id);

        Person Update(Person aPerson);

        bool Delete(Person aPerson);
    }
}
