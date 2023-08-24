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
using AbyssMoth.Internal.Codebase.Cookies;
using AbyssMoth.Internal.Codebase.Localization;
using AbyssMoth.Internal.Codebase.Services.Fate;
using AbyssMoth.Internal.Codebase.Services.Localization;
using AbyssMoth.Internal.Codebase.Services.TextReader;
using AbyssMoth.Internal.Codebase.SettingsPanel;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Boot
{
    public sealed class Bootstrupper : MonoBehaviour
    {
        [SerializeField] private CookiesController cookiesController;
        [SerializeField] private MoreCookiesPanel moreCookiesPanel;
        [SerializeField] private SettingsPanelView settingsPanel;

        [SerializeField , HideInInspector] private List<LocalizationTextComponent> localizationTextComponents;

        private IFateServices fateServices;
        private ITextFileReaderServices textFileReaderServices;
        private ILocalizationServices localizationServices;

        private void Awake()
        {
            localizationServices = new LocalizationServices(LanguageTypeID.Ru);
            textFileReaderServices = new TextFileReaderServicesServices(localizationServices);
            fateServices = new FateServices(textFileReaderServices, localizationServices);
        }

        private void Start()
        {
            fateServices.Initialize();
            cookiesController.Initialization(fateServices);
            settingsPanel.Initialize(localizationServices);

            foreach (var localizationText in localizationTextComponents)
                localizationText.Initialize(localizationServices); 
            
            moreCookiesPanel.Initialize();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate()
        {
            localizationTextComponents = new List<LocalizationTextComponent>();

            foreach (var localizationText in FindObjectsOfType<LocalizationTextComponent>(true))
                localizationTextComponents.Add(localizationText);
        }
    }
}