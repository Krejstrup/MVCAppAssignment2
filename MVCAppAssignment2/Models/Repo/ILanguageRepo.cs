using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System.Collections.Generic;

namespace MVCAppAssignment2.Models.Repo
{
    public interface ILanguageRepo
    {

        // Let's have some new C.R.U.D.:

        Language Create(CreateLanguage aLanguage);

        List<Language> Read();

        Language Read(int id);

        Language Update(Language aLanguage);

        bool Delete(Language aLanguage);
    }
}
