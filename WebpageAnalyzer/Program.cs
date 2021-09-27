using System;

using WebpageAnalyzer.Log;
using WebpageAnalyzer.Components;
namespace WebpageAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Показывать лог в консоли (в реальном времени)
            Logger.WriteToConsole = true;

            // Запросить у пользователя путь к файлу
            Console.WriteLine("Укажите путь до файла, которое потребуется обработать");
            string inputFilePath = Console.ReadLine();

            // Запуск анализатора с указанием IReader, ICleaner, IWriter
            // В данном случае используется:
            // FileReader для чтения исходного текста,
            // HtmlCleaner для удаления HTML тегов и символов
            Analyzer analyzer = new Analyzer(new FileReader(inputFilePath), new HtmlCleaner(), null);
            analyzer.WriteResultsToConsole = true;
            analyzer.Process();
        }
    }
}
