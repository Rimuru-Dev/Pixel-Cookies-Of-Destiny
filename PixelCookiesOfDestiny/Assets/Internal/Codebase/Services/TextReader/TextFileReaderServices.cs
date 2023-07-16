// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.IO;
using AbyssMoth.Internal.Codebase.Constants;
using AbyssMoth.Internal.Codebase.Services.Localization;

namespace AbyssMoth.Internal.Codebase.Services.TextReader
{
    public sealed class TextFileReaderServicesServices : ITextFileReaderServices
    {
        private readonly ILocalizationServices localizationServices;

        public TextFileReaderServicesServices(ILocalizationServices localizationServices) =>
            this.localizationServices = localizationServices ?? throw new ArgumentNullException(nameof(localizationServices));

        public string[] GetTextDataset()
        {
            var filePath = AssetPath.TextDatasets + localizationServices.GetCurrentLanguage() + ".txt";

            if (!File.Exists(filePath))
                throw new NullReferenceException("Datasets not found.");

            var fileText = File.ReadAllText(filePath);

            return fileText.Split('\n');
        }
    }
}