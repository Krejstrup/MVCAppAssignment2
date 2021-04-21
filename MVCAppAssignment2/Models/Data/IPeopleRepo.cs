using System.Collections.Generic;

namespace MVCAppAssignment2.Models.Data
{
    interface IPeopleRepo
    {
        Person Create(string firstName, string lastName, string phone, string city);

        List<Person> Read();

        Person Read(int id);

        Person Update(Person person);

        bool Delete(Person Person);
    }
}
