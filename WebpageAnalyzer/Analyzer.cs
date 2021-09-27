using System;
using System.Collections.Generic;
using System.IO;

using WebpageAnalyzer.Components;
using WebpageAnalyzer.Log;
namespace WebpageAnalyzer
{
    /// <summary>
    /// 
    /// </summary>
    public class Analyzer
    {
        /// <summary>
        /// Выводить ли результаты анализа в консоль
        /// </summary>
        public bool WriteResultsToConsole { get; set; } = false;

        /// <summary>
        /// Сохранять ли лог в файлы
        /// </summary>
        public bool SaveLog { get; set; } = false;

        protected IReader Reader { get; set; }
        protected Splitter Splitter { get; set; } = new Splitter(new List<char>() { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' });
        protected ICleaner Cleaner { get; set; }
        protected Counter Counter { get; set; } = new Counter();
        protected Sorter Sorter { get; set; } = new Sorter();
        protected IWriter Writer { get; set; }

        /// <exception cref="ArgumentNullException">Если какой-либо параметр имел пустое значение</exception>
        public Analyzer(IReader reader, ICleaner cleaner, IWriter writer)
        {
            if(reader == null || cleaner == null)
                throw new ArgumentNullException("Компоненты reader и writer не могут иметь пустое значение");

            this.Reader = reader;
            this.Cleaner = cleaner;
            this.Writer = writer;
        }

        /// <summary>
        /// Запустить процесс: Прочитать, Разбить, Очистить, Посчитать, Отсортировать, Записать
        /// </summary>
        public void Process()
        {
            try
            {
                // Прочитать файл
                string text = this.Reader.Read();

                // Разбить текст на слова
                string[] words = this.Splitter.Split(text);

                // Очистить текст от HTML тегов и аттрибутов
                words = this.Cleaner.Clean(words);

                // Посчитать слова (И отсортировать)
                Dictionary<string, int> stats = this.Counter.Count(words);

                // Отсортировать
                stats = this.Sorter.Sort(stats);

                // Вывести в консоль
                if(this.WriteResultsToConsole)
                    foreach (string word in stats.Keys)
                        Console.WriteLine($"{word} - {stats[word]}");

                // Сохранить статистику (если требуется)
                if (this.Writer != null)
                    this.Writer.Write(stats);
            }
            catch (Exception exception)
            {
                // Вывод внештатного исключения в лог
                Logger.Add(MessageType.Error, exception.Message);
            }

            Logger.Add(MessageType.Info, "Программа закончила свою работу");

            // Сохранить лог (Log.txt - весь лог, Errors.txt - лог ошибок)
            if(SaveLog)
            {
                Logger.Save(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Log.txt", false);
                Logger.Save(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Errors.txt", true);
            }
        }
    }
}
