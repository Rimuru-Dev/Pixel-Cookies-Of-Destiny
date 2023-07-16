// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

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