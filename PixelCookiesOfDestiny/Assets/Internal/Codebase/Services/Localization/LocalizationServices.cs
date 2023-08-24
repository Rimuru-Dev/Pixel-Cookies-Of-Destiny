// ReSharper disable All
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
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