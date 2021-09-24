using System;
using System.Collections.Generic;
using System.Linq;

using WebpageAnalyzer.Log;
namespace WebpageAnalyzer.Tools
{
    /// <summary>
    /// Помогает посчитать количество разных слов в массиве и отсортировать
    /// </summary>
    public sealed class Counter
    {
        /// <summary>
        /// Важен ли регистр у слов?
        /// </summary>
        public bool CaseSensetive { get; set; } = false;

        public Counter()
        { }

        /// <summary>
        /// Подсчитать количество каждого слова в массиве (и отсортировать если требуется)
        /// </summary>
        /// <param name="words">Массив слов</param>
        /// <param name="sort">Сортировать ли результат</param>
        /// <returns>Отсортированный словарь с указанием сколько раз встречалось каждое слово</returns>
        /// <exception cref="ArgumentNullException">Если слова для подсчета не были предоставлены</exception>
        public Dictionary<string, int> Count(string[] words, bool sort = true)
        {
            if (words == null || words.Length < 1)
                throw new ArgumentNullException("Слова для подсчета не были предоставлены");

            Dictionary<string, int> stats = new Dictionary<string, int>();

            // Подсчитать слова
            for (var i = 0; i < words.Length; i++)
            {
                string currentWord = words[i];

                // Если не важен регистр, использовать верхний
                if (!CaseSensetive)
                    currentWord = currentWord.ToUpper();

                // Если слово уже добавлено в статистику, увеличить значение на 1
                if (stats.ContainsKey(currentWord))
                    stats[currentWord]++;
                // Иначе добавить в статистику, указать, что встретилось 1 раз
                else
                    stats.Add(currentWord, 1);

                Logger.Add(MessageType.Info, $"Слово '{currentWord}' в тексте фигурирует в {stats[currentWord]} раз'");
            }

            // Если требуется отсортировать
            if(sort)
                stats.OrderBy(stat => stat.Value).ToDictionary(stat => stat.Key, stat => stat.Value);

            return stats;
        }
    }
}
