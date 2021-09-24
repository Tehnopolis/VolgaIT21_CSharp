using System;
using System.Collections.Generic;

namespace WebpageAnalyzer.Tools
{
    /// <summary>
    /// Помогает разбивать текст на слова указанными символами
    /// </summary>
    public sealed class Splitter
    {
        /// <summary>
        /// Символы, которыми текст будет разбит на слова
        /// </summary>
        public List<char> Splitters { get; set; }

        public Splitter()
        {
            this.Splitters = new List<char>();
        }
        public Splitter(List<char> splitters)
        {
            this.Splitters = splitters;
        }

        /// <summary>
        /// Разбивает текст на слова
        /// </summary>
        /// <param name="text">Входной текст</param>
        /// <returns>Массив из слов</returns>
        /// <exception cref="InvalidOperationException">Если не были предоставлены символы для разбиения текста</exception>
        /// <exception cref="ArgumentNullException">Если предоставленный текст был пустым</exception>
        public string[] Split(string text)
        {
            if (this.Splitters == null || this.Splitters.Count < 1)
                throw new InvalidOperationException("Символы для разбиения текста не были предоставлены");
            else if (text == null || text.Trim() == "")
                throw new ArgumentNullException("Предоставленный текст был пустым");
            else
                return text.Split(this.Splitters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
