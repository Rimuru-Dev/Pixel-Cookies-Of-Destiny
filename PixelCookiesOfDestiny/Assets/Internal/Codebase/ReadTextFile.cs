// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.IO;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase
{
    public class ReadTextFile : MonoBehaviour
    {
        void Start()
        {
            // Путь к файлу
            string filePath = Path.Combine(Application.dataPath, "Resources/Datasets/file.txt");

            // Проверяем, существует ли файл
            if (File.Exists(filePath))
            {
                // Читаем весь файл
                string[] lines = File.ReadAllLines(filePath);

                // Выводим каждую строку
                foreach (string line in lines)
                {
                    Debug.Log(line);
                }
            }
            else
            {
                Debug.LogError("Файл не найден!");
            }
        }
    }
}
