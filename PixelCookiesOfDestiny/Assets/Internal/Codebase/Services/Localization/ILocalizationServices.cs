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

namespace AbyssMoth.Internal.Codebase.Services.Localization
{
    public interface ILocalizationServices
    {
        public Action<LanguageTypeID> OnLanguageChanged { get; set; }
        public string GetCurrentLanguage();
        public void SwitchLandiage(LanguageTypeID languageTypeID);
    }
}