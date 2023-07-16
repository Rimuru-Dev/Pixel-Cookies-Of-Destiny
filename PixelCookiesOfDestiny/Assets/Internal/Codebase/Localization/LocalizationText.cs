// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
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