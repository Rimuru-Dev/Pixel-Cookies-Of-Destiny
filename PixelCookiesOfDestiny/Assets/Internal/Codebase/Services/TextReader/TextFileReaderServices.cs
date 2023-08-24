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
using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Services.TextReader
{
    public sealed class TextFileReaderServicesServices : ITextFileReaderServices
    {
        private readonly ILocalizationServices localizationServices;

        public TextFileReaderServicesServices(ILocalizationServices localizationServices) =>
            this.localizationServices =
                localizationServices ?? throw new ArgumentNullException(nameof(localizationServices));

        // Note: Only for PC and Mobile platform. In WEB not work.
        // public string[] GetTextDataset()
        // {
        //     var filePath = AssetPath.TextDatasets + localizationServices.GetCurrentLanguage() + ".txt";
        //
        //     if (!File.Exists(filePath))
        //         throw new NullReferenceException("Datasets not found.");
        //
        //     var fileText = File.ReadAllText(filePath);
        //
        //     return fileText.Split('\n');
        // }


        // Note: WEB GL + PC + Mobile
        // TODO: Remove hardcode strings.
        public string[] GetTextDataset()
        {
            var currentLanguage = localizationServices.GetCurrentLanguage();

            var textAsset = Resources.Load<TextAsset>($"Datasets/{currentLanguage}");

            return textAsset == null
                ? Resources.Load<TextAsset>("Datasets/ru").text.Split('\n')
                : textAsset.text.Split('\n');
        }
    }
}