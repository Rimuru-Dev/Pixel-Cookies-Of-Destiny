// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using AbyssMoth.Internal.Codebase.Services.Fate;
using AbyssMoth.Internal.Codebase.Services.Localization;
using AbyssMoth.Internal.Codebase.Services.TextReader;
using AbyssMoth.Internal.Codebase.View;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Boot
{
    public sealed class Bootstrupper : MonoBehaviour
    {
        [SerializeField] private LanguageTypeID defaultLanguage;
        [SerializeField] private CookiesView cookiesView;
        
        private IFateServices fateServices;
        private ITextFileReaderServices textFileReaderServices;
        private ILocalizationServices localizationServices;
        
        private void Awake()
        {
            localizationServices = new LocalizationServices(LanguageTypeID.Tr);
            textFileReaderServices = new TextFileReaderServicesServices(localizationServices);
            fateServices = new FateServices(textFileReaderServices,localizationServices);
        }

        private void Start()
        {
            fateServices.Initialize();
            cookiesView.Initialization(fateServices);
        }
    }
}