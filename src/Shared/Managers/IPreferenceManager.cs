﻿using EPharma.Shared.Settings;
using System.Threading.Tasks;
using EPharma.Shared.Wrapper;

namespace EPharma.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}