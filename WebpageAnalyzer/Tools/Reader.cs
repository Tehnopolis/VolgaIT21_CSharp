using System;
using System.IO;

namespace WebpageAnalyzer.Tools
{
    /// <summary>
    /// Помогает читать файл безопасно
    /// </summary>
    public sealed class Reader
    {
        private string FilePath;

        /// <exception cref="ArgumentNullException">Если не был предоставлен путь до файла</exception>
        public Reader(string path)
        {
            if(path == null || path == "")
                throw new ArgumentNullException("Путь до файла не был предоставлен");

            this.FilePath = path;
        }

        /// <summary>
        /// Прочитать файл
        /// </summary>
        /// <returns>Текст файла</returns>
        public string Read()
        {
            // Прочитать файл
            string result = null;
            StreamReader reader = new StreamReader(this.FilePath);
            result = reader.ReadToEnd();
            reader.Close();

            // Выдать текст файла
            return result;
        }
    }
}
