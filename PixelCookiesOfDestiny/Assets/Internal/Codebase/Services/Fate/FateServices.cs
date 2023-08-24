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
using AbyssMoth.Internal.Codebase.Services.TextReader;

namespace AbyssMoth.Internal.Codebase.Services.Fate
{
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
}