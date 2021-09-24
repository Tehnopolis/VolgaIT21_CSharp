using System;
using System.IO;
using System.Collections.Generic;

using WebpageAnalyzer.Log;
using WebpageAnalyzer.Tools;
using System.Text.RegularExpressions;

namespace WebpageAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.WriteToConsole = true;

            Console.WriteLine("Укажите путь до файла, которое потребуется обработать");
            string inputFilePath = Console.ReadLine();

            try
            {
                // Прочитать файл
                string text = new Reader(inputFilePath).Read();

                // Разбить текст на слова
                Splitter splitter = new Splitter();
                splitter.Splitters = new List<char>() { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' };
                string[] words = splitter.Split(text);

                // Очистить текст от HTML тегов и аттрибутов
                Cleaner cleaner = new Cleaner((word) => {
                    // Убрать лишние символы
                    var replaced = Regex.Replace(word, @"[a-zA-Z0-9]*[=|&|<|>|\/]*", "");
                    // Если слово длинной больше 1, то возвратить его
                    return replaced.Length > 1 ? replaced : "";
                });
                words = cleaner.Clean(words);

                // Посчитать слова (И отсортировать)
                Counter counter = new Counter();
                Dictionary<string, int> stats = counter.Count(words);

                // Вывести в консоль
                foreach (string word in stats.Keys)
                {
                    int amount = stats[word];

                    Console.WriteLine($"{word} - {amount}");
                }

            }
            catch (Exception exception)
            {
                // Вывод внештатного исключения в лог
                Logger.Add(MessageType.Error, exception.Message);
            }

            Logger.Add(MessageType.Info, "Программа закончила свою работу");

            // Сохранить лог (Log.txt - весь лог, Errors.txt - лог ошибок)
            Logger.Save(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Log.txt", false);
            Logger.Save(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Errors.txt", true);
        }
    }
}
