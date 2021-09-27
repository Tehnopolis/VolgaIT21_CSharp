using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using WebpageAnalyzer.Log;
namespace WebpageAnalyzer.Components
{
    /// <summary>
    /// Помогает очищать массив слов с помощью регулярных выражений (RegExp)
    /// </summary>
    public sealed class CustomRegexCleaner : ICleaner
    {
        private string RegexString;

        /// <param name="regex">Регулярное выражение RegExp</param>
        /// <exception cref="ArgumentNullException">Если регулярное выражение не было предоставлено</exception>
        public CustomRegexCleaner(string regex)
        {
            if (regex == null || regex == "")
                throw new ArgumentNullException("Регулярное выражение не было предоставлено");

            this.RegexString = regex;
        }

        /// <summary>
        /// Отфильтровать массив слов 
        /// </summary>
        /// <param name="words">Массив слов</param>
        /// <returns>Отфильтрованный массив слов</returns>
        /// <exception cref="ArgumentNullException">Если слова для фильтрации не были предоставлены</exception>
        public string[] Clean(string[] words)
        {
            if (words == null || words.Length < 1)
                throw new ArgumentNullException("Слова для фильтрации не были предоставлены");

            List<string> filteredWords = new List<string>();

            // Пройтись по каждому слову
            for (var i = 0; i < words.Length; i++)
            {
                string currentWord = words[i];
                string filteredWord = Regex.Replace(currentWord, this.RegexString, "");
                if (filteredWord.Trim().Length < 1)
                    filteredWord = "";

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
