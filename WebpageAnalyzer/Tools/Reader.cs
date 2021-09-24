using System;
using System.IO;
using System.Text;

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
        public string Read(Int32 bufferSize = 128)
        {
            if (bufferSize == 0)
                throw new ArgumentNullException("Размер буффера чтения файла был указан неверно");

            // Прочитать файл
            StringBuilder textBuilder = new StringBuilder();
            using (var fileStream = File.OpenRead(this.FilePath))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    // StringBuilder.Capacity определяет сколько символов может хранится в памяти экземпляра
                    if(textBuilder.Length + line.Length >= textBuilder.Capacity)
                        textBuilder.AppendLine(line);
                }
            }

            // Выдать текст файла
            return textBuilder.ToString();
        }
    }
}
