using DynaCore.Shared.Settings;
using System.Threading.Tasks;
using DynaCore.Shared.Wrapper;

namespace DynaCore.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}