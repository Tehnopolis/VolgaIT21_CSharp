using System;
using System.Collections.Generic;

using WebpageAnalyzer.Log;
namespace WebpageAnalyzer.Tools
{
    /// <summary>
    /// Помогает очищать(фильтровать) массив слов от лишних
    /// </summary>
    public sealed class Cleaner
    {
        /// <summary>
        /// Фильтр слов, изменяет слово в нужный формат
        /// </summary>
        /// <param name="word">Слово</param>
        /// <returns>Изменненое слово или ничего (Если убрать слово вовсе)</returns>
        public delegate string WordFilter(string word);

        public WordFilter Filter { get; set; }

        public Cleaner(WordFilter filter)
        {
            this.Filter = filter;
        }

        /// <summary>
        /// Отфильтровать массив слов 
        /// </summary>
        /// <param name="words">Массив слов</param>
        /// <returns>Отфильтрованный массив слов</returns>
        /// <exception cref="InvalidOperationException">Если фильтр слов не был определен</exception>
        /// <exception cref="ArgumentNullException">Если слова для фильтрации не были предоставлены</exception>
        public string[] Clean(string[] words)
        {
            if (this.Filter == null)
                throw new InvalidOperationException("Фильтр слов не был определен");
            else if (words == null || words.Length < 1)
                throw new ArgumentNullException("Слова для фильтрации не были предоставлены");

            List<string> filteredWords = new List<string>();

            // Пройтись по каждому слову
            for(var i = 0; i < words.Length; i++)
            {
                string currentWord = words[i];
                string filteredWord = this.Filter(currentWord);

                // Если фильтр слов разрешил слово
                if (filteredWord != null && filteredWord != "")
                {
                    filteredWords.Add(filteredWord);
                    Logger.Add(MessageType.Info, $"Слово было отфильтровано: '{currentWord}' => '{filteredWord}'");
                }
                else
                    Logger.Add(MessageType.Info, $"Слово было убрано: '{currentWord}'");
            }

            return filteredWords.ToArray();
        }
    }
}
