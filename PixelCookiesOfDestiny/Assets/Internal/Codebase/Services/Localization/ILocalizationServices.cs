using System;

namespace AbyssMoth.Internal.Codebase.Services.Localization
{
    public interface ILocalizationServices
    {
        public Action<LanguageTypeID> OnLanguageChanged { get; set; }
        public string GetCurrentLanguage();
        public void SwitchLandiage(LanguageTypeID languageTypeID);
    }
}