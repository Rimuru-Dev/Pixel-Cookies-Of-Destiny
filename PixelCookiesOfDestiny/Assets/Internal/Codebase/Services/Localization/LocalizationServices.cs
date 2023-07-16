// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using AbyssMoth.Internal.Codebase.Constants;

namespace AbyssMoth.Internal.Codebase.Services.Localization
{
    public sealed class LocalizationServices : ILocalizationServices
    {
        public Action<LanguageTypeID> OnLanguageChanged { get; set; }
        private LanguageTypeID languageTypeID;

        public LocalizationServices(LanguageTypeID languageTypeID) =>
            this.languageTypeID = languageTypeID;

        public string GetCurrentLanguage()
        {
            return languageTypeID switch
            {
                LanguageTypeID.Ru => Languages.Ru,
                LanguageTypeID.En => Languages.En,
                LanguageTypeID.Tr => Languages.Tr,
                _ => Languages.Ru
            };
        }

        public void SwitchLandiage(LanguageTypeID languageTypeID)
        {
            this.languageTypeID = languageTypeID;
            OnLanguageChanged?.Invoke(this.languageTypeID);
        }
    }
}