using System;
using System.IO;
using System.Text;

namespace WebpageAnalyzer.Components
{
    /// <summary>
    /// Помогает читать файл безопасно
    /// </summary>
    public sealed class FileReader : IReader
    {
        private string FilePath;
        private Int32 BufferSize;

        /// <exception cref="ArgumentNullException">Если не был предоставлен путь до файла</exception>
        public FileReader(string filePath, Int32 bufferSize = 128)
        {
            if (filePath == null || filePath == "")
                throw new ArgumentNullException("Путь до файла не был предоставлен");
            if (bufferSize == 0)
                throw new ArgumentNullException("Размер буффера чтения файла был указан неверно");

            this.FilePath = filePath;
            this.BufferSize = bufferSize;
        }

        /// <summary>
        /// Прочитать файл
        /// </summary>
        /// <returns>Текст файла</returns>
        /// <exception cref="OutOfMemoryException">Если файл имел слишком много символов</exception>
        public string Read()
        {
            // Прочитать файл
            StringBuilder textBuilder = new StringBuilder();
            using (var fileStream = File.OpenRead(this.FilePath))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, this.BufferSize))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        // StringBuilder.Capacity определяет сколько символов может хранится в памяти экземпляра
                        if (textBuilder.Length + line.Length >= textBuilder.Capacity)
                            textBuilder.AppendLine(line);
                        else
                            throw new OutOfMemoryException();
                    }
                }

            // Выдать текст файла
            return textBuilder.ToString();
        }
    }
}
