using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;

namespace MVCAppAssignment2.Models.Service
{
    public interface ILanguageService
    {
        public Language Add(CreateLanguage aLanguage);

        public Languages All();

        public Language FindBy(int id);

        public Language Edit(int id, Language aLanguage);

        public bool Remove(int id);
    }
}
