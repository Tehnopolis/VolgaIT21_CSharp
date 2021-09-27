using System;
using System.Collections.Generic;

namespace WebpageAnalyzer.Components
{
    /// <summary>
    /// Помогает разбивать текст на слова указанными символами
    /// </summary>
    public sealed class Splitter
    {
        /// <summary>
        /// Символы, которыми текст будет разбит на слова
        /// </summary>
        public List<char> SplitSymbols { get; set; }

        public Splitter()
        {
            this.SplitSymbols = new List<char>();
        }
        /// <param name="splitters">Символы-разбиватели слов</param>
        public Splitter(List<char> splitters)
        {
            this.SplitSymbols = splitters;
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
            if (this.SplitSymbols == null || this.SplitSymbols.Count < 1)
                throw new InvalidOperationException("Символы для разбиения текста не были предоставлены");
            else if (text == null || text.Trim() == "")
                throw new ArgumentNullException("Предоставленный текст был пустым");
            else
                return text.Split(this.SplitSymbols.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
