using System.Linq;
using EPharma.Shared.Constants.Localization;
using EPharma.Shared.Settings;

namespace EPharma.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}