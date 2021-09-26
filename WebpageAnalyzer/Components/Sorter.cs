using System;
using System.Collections.Generic;
using System.Linq;

namespace WebpageAnalyzer.Components
{
    /// <summary>
    /// Помогает отсортировать статистику
    /// </summary>
    public sealed class Sorter
    {
        public Sorter()
        { }

        /// <summary>
        /// Отсортировать слова от наиболее к наименее часто встречающимся
        /// </summary>
        /// <param name="words">Неотсортированный словарь слов</param>
        /// <returns>Отсортированный словарь</returns>
        /// <exception cref="ArgumentNullException">Если словарь с словами для сортировки не был предоставлен</exception>
        public Dictionary<string, int> Sort(Dictionary<string, int> words)
        {
            if (words == null || words.Count < 1)
                throw new ArgumentNullException("Слова для подсчета не были предоставлены");

            // Отсортировать (Descending)
            return words.OrderByDescending(key => key.Value).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
