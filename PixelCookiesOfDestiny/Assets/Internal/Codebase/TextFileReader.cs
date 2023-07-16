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

    public sealed class TextFileReader : MonoBehaviour
    {
        [SerializeField] private LanguageTypeID languageTypeID = LanguageTypeID.Ru;
        [SerializeField] private string[] textLines;
        private int index = -1;
    
        private void Awake()
        {
            languageTypeID = LanguageTypeID.Ru;
            GetTextDataset();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                SwitchLandiage(LanguageTypeID.Ru);

            if (Input.GetKeyDown(KeyCode.E))
                SwitchLandiage(LanguageTypeID.En);

            if (Input.GetKeyDown(KeyCode.T))
                SwitchLandiage(LanguageTypeID.Tr);
        }

        private void GetTextDataset()
        {
            var filePath = "Assets/Internal/Resources/Datasets/" + GetCurrentLanguage() + ".txt";

            if (File.Exists(filePath))
            {
                var fileText = File.ReadAllText(filePath);
                textLines = fileText.Split('\n');
            }
            else
                Debug.LogWarning("Файл с текстом не найден для выбранного языка");
        }

        private void SwitchLandiage(LanguageTypeID languageTypeID)
        {
            index = 0;
            this.languageTypeID = languageTypeID;
            GetTextDataset();
        }

        public string GetFate()
        {
            index++;

            if (index < textLines.Length)
            {
                return textLines[index];
            }

            return textLines[Random.Range(0, textLines.Length)];
        }

        private string GetCurrentLanguage()
        {
            return languageTypeID switch
            {
                LanguageTypeID.Ru => "ru",
                LanguageTypeID.En => "en",
                LanguageTypeID.Tr => "tr",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}