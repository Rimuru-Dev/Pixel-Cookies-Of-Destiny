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

using AbyssMoth.Internal.Codebase.Services.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.SettingsPanel
{
    public sealed class SettingsPanelView : MonoBehaviour
    {
        [field: SerializeField] public Button RuButton { get; private set; }
        [field: SerializeField] public Button EnButton { get; private set; }
        [field: SerializeField] public Button TrButton { get; private set; }

        private ILocalizationServices localizationServices;

        public void Initialize(ILocalizationServices localizationServices)
        {
            this.localizationServices = localizationServices;

            RuButton.onClick.AddListener(() => { localizationServices.SwitchLandiage(LanguageTypeID.Ru); });
            EnButton.onClick.AddListener(() => { localizationServices.SwitchLandiage(LanguageTypeID.En); });
            TrButton.onClick.AddListener(() => { localizationServices.SwitchLandiage(LanguageTypeID.Tr); });
        }

        private void OnDestroy()
        {
            RuButton.onClick.RemoveAllListeners();
            EnButton.onClick.RemoveAllListeners();
            TrButton.onClick.RemoveAllListeners();
        }
    }
}