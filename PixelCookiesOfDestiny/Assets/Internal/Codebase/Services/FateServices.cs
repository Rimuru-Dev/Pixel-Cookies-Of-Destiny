﻿// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;

namespace AbyssMoth.Internal.Codebase
{
    public interface IInitialialize
    {
        public void Initialize();
    }

    public sealed class FateServices : IFateServices
    {
        private readonly ITextFileReaderServices fileReaderServices;
        private readonly ILocalizationServices localizationServices;

        private int index = -1;
        private string[] textLines;

        public FateServices(ITextFileReaderServices fileReaderServices, ILocalizationServices localizationServices)
        {
            this.fileReaderServices = fileReaderServices ?? throw new ArgumentNullException(nameof(fileReaderServices));
            this.localizationServices =
                localizationServices ?? throw new ArgumentNullException(nameof(localizationServices));
        }

        public void Initialize()
        {
            localizationServices.OnLanguageChanged += OnLanguageChanged;
            textLines = fileReaderServices.GetTextDataset();
        }

        ~FateServices()
        {
            textLines = null;
            localizationServices.OnLanguageChanged -= OnLanguageChanged;
        }

        public string GetFate()
        {
            index++;

            return index < textLines.Length
                ? textLines[index]
                : textLines[UnityEngine.Random.Range(0, textLines.Length)];
        }

        private void OnLanguageChanged(LanguageTypeID languageTypeID)
        {
            index = 0;
            textLines = fileReaderServices.GetTextDataset();
        }
    }

    public sealed class LocalizationServices : ILocalizationServices
    {
        private LanguageTypeID languageTypeID;
        public Action<LanguageTypeID> OnLanguageChanged { get; set; }

        public LocalizationServices(LanguageTypeID languageTypeID)
        {
            this.languageTypeID = languageTypeID;
        }

        public string GetCurrentLanguage()
        {
            return languageTypeID switch
            {
                LanguageTypeID.Ru => "ru",
                LanguageTypeID.En => "en",
                LanguageTypeID.Tr => "tr",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SwitchLandiage(LanguageTypeID languageTypeID)
        {
            this.languageTypeID = languageTypeID;
        }
    }

    public interface IFateServices : IInitialialize
    {
        public string GetFate();
    }

    public interface ILocalizationServices
    {
        public Action<LanguageTypeID> OnLanguageChanged { get; set; }
        public string GetCurrentLanguage();
        public void SwitchLandiage(LanguageTypeID languageTypeID);
    }
}