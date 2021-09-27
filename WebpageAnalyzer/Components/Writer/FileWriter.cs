using System;
using System.Collections.Generic;
using System.IO;

namespace WebpageAnalyzer.Components
{
    public enum FileFormat
    {
        Text,
        JSON
    }

    /// <summary>
    /// Помогает сохранять результат в виде текстового файла
    /// </summary>
    public sealed class FileWriter : IWriter
    {
        private string FilePath;
        private FileFormat Format;

        /// <param name="filePath">Путь к файлу</param>
        /// <param name="format">Формат файла</param>
        /// <exception cref="ArgumentNullException">Если не был предоставлен путь до файла</exception>
        public FileWriter(string filePath, FileFormat format)
        {
            if (filePath == null || filePath == "")
                throw new ArgumentNullException("Путь до файла не был предоставлен");

            this.FilePath = filePath;
            this.Format = format;
        }


        /// <summary>
        /// Сохранить в файл
        /// </summary>
        public void Write(Dictionary<string, int> data)
        {
            // Открыть запись в файл
            StreamWriter writer = new StreamWriter(this.FilePath);

            // Записать все данные
            switch (this.Format)
            {
                case FileFormat.Text:
                    foreach (string word in data.Keys)
                        writer.WriteLine($"{word} - {data[word]}");
                    break;
                case FileFormat.JSON:
                    //TODO
                    break;
            }

            // Закрыть запись в файл
            writer.Close();
        }
    }
}
