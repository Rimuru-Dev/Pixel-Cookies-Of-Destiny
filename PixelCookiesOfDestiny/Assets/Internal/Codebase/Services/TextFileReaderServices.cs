// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AbyssMoth.Internal.Codebase
{
    [Serializable]
    public enum LanguageTypeID
    {
        Ru = 0,
        En = 1,
        Tr = 2,
    }

    public sealed class TextFileReaderServicesServices : ITextFileReaderServices
    {
        private readonly ILocalizationServices localizationServices;

        public TextFileReaderServicesServices(ILocalizationServices localizationServices)
        {
            this.localizationServices = localizationServices;
        }

        public string[] GetTextDataset()
        {
            var filePath = "Assets/Internal/Resources/Datasets/" + localizationServices.GetCurrentLanguage() + ".txt";

            if (!File.Exists(filePath))
                throw new NullReferenceException("Datasets not found.");

            var fileText = File.ReadAllText(filePath);

            return fileText.Split('\n');
        }
    }

    public interface ITextFileReaderServices
    {
        public string[] GetTextDataset();
    }
}