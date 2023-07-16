// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase
{
    public sealed class Bootstrupper : MonoBehaviour
    {
        [SerializeField] private CookiesView cookiesView;
        
        private IFateServices fateServices;
        private ITextFileReaderServices textFileReaderServices;
        private ILocalizationServices localizationServices;
        
        private void Awake()
        {
            localizationServices = new LocalizationServices(LanguageTypeID.Ru);
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