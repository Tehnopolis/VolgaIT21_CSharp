using System;
using System.IO;
using System.Collections.Generic;

using WebpageAnalyzer.Log;
using WebpageAnalyzer.Components;
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

            Analyzer analyzer = new Analyzer(new FileReader(inputFilePath), new HtmlCleaner(), null);
            analyzer.WriteResultsToConsole = true;
            analyzer.Process();
        }
    }
}
