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
using AbyssMoth.Internal.Codebase.Services.Localization;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Localization
{
    [Serializable]
    public sealed class LocalizationText
    {
        [field: SerializeField] public LanguageTypeID Language { get; set; }
        [field: SerializeField] public string Text { get; set; }
    }
}