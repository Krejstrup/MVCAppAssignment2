using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;


namespace MVCAppAssignment2.Models.Service
{
    public interface IPeopleService
    {
        public Person Add(CreatePerson person);

        public PersonLanguage AddLanguageToPerson(int perId, int langId);

        public bool RemoveLanguageFromPerson(int perId, int langId);

        public People All();

        public People ApiAll();
        public People FindBy(People search);

        public Person FindBy(int id);

        public Person Edit(int id, Person person);

        public Person Edit(int id, EditPerson person);
        public bool Remove(int id);


    }
}
