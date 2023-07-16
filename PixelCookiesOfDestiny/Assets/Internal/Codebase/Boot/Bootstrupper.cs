// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using AbyssMoth.Internal.Codebase.Cookies;
using AbyssMoth.Internal.Codebase.Services.Fate;
using AbyssMoth.Internal.Codebase.Services.Localization;
using AbyssMoth.Internal.Codebase.Services.TextReader;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Boot
{
    public sealed class Bootstrupper : MonoBehaviour
    {
        [SerializeField] private CookiesController cookiesController;
        [SerializeField] private MoreCookiesPanel moreCookiesPanel;

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
            moreCookiesPanel.Initialize();
        }
    }
}