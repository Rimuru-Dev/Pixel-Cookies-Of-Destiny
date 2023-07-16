// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Linq;
using AbyssMoth.Internal.Codebase.Services.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.Localization
{
    public sealed class LocalizationTextComponent : MonoBehaviour
    {
        [field: SerializeField] public List<LocalizationText> LocalizationTextList { get; private set; }

        [field: SerializeField, HideInInspector]
        public Text Text { get; private set; }

        private ILocalizationServices localizationServices;

        public void Initialize(ILocalizationServices localizationServices)
        {
            this.localizationServices = localizationServices;
            localizationServices.OnLanguageChanged += LanguageChanged;
        }

        private void Reset()
        {
            Text ??= GetComponent<Text>();

            LocalizationTextList = new List<LocalizationText>();
            LocalizationTextList.Add(new LocalizationText
            {
                Language = LanguageTypeID.Ru,
                Text = Text.text
            });

            LocalizationTextList.Add(new LocalizationText
            {
                Language = LanguageTypeID.En,
            });

            LocalizationTextList.Add(new LocalizationText
            {
                Language = LanguageTypeID.Tr,
            });
        }

        private void OnValidate()
        {
            if (LocalizationTextList is not { Count: > 0 })
                Reset();

            Text ??= GetComponent<Text>();
        }

        private void OnDestroy() => 
            localizationServices.OnLanguageChanged -= LanguageChanged;

        private void LanguageChanged(LanguageTypeID languageTypeID)
        {
            Text.text = languageTypeID switch
            {
                LanguageTypeID.Ru => LocalizationTextList
                    .FirstOrDefault(language => languageTypeID == LanguageTypeID.Ru)
                    ?.Text,
                LanguageTypeID.En => LocalizationTextList
                    .FirstOrDefault(language => languageTypeID == LanguageTypeID.En)
                    ?.Text,
                LanguageTypeID.Tr => LocalizationTextList
                    .FirstOrDefault(language => languageTypeID == LanguageTypeID.Tr)
                    ?.Text,
                _ => throw new ArgumentOutOfRangeException(nameof(languageTypeID), languageTypeID, null)
            };
        }
    }
}