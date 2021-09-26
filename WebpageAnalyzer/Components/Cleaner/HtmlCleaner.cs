using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using WebpageAnalyzer.Log;
namespace WebpageAnalyzer.Components
{
    /// <summary>
    /// Помогает очищать массив слов от HTML тегов и символов
    /// </summary>
    public sealed class HtmlCleaner : ICleaner
    {
        private delegate string WordFilter(string word);
        private WordFilter Filter { get; set; } = (word) => {
            // Убрать лишние символы
            var replaced = Regex.Replace(word, @"[a-zA-Z0-9]*[=|&|<|>|\/]*", "");
            // Если слово длинной больше 1, то возвратить его
            return replaced.Length > 1 ? replaced : "";
        };

        public HtmlCleaner() { }

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
            for (var i = 0; i < words.Length; i++)
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
