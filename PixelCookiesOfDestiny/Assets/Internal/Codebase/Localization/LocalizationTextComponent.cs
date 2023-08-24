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

        [field: SerializeField, HideInInspector] public Text Text { get; private set; }

        private ILocalizationServices localizationServices;

        public void Initialize(ILocalizationServices localizationServices)
        {
            this.localizationServices = localizationServices;
            localizationServices.OnLanguageChanged += LanguageChanged;
        }

        [System.Diagnostics.Conditional("DEBUG")]
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

        [System.Diagnostics.Conditional("DEBUG")]
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
            foreach (var localizationText in LocalizationTextList.Where(x => x.Language == languageTypeID))
                Text.text = localizationText.Text;
        }
    }
}