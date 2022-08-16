using System.Linq;
using DynaCore.Shared.Constants.Localization;
using DynaCore.Shared.Settings;

namespace DynaCore.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}