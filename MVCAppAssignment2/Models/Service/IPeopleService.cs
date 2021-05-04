using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;


namespace MVCAppAssignment2.Models.Service
{
    public interface IPeopleService
    {
        public Person Add(CreatePerson person);

        public People All();

        public People FindBy(People search);

        public Person FindBy(int id);

        public Person Edit(int id, Person person);

        public bool Remove(int id);


    }
}
