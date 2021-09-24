using System;
using System.IO;
using System.Collections.Generic;

namespace WebpageAnalyzer.Log
{
    /// <summary>
    /// Содержит лог
    /// </summary>
    public static class Logger
    {
        private static List<LogMessage> Messages = new List<LogMessage>();

        /// <summary>
        /// Выводить ли лог в консоль
        /// </summary>
        public static bool WriteToConsole { get; set; } = false;

        /// <summary>
        /// Добавить сообщение в лог
        /// </summary>
        /// <param name="messageType">Тип сообщения</param>
        /// <param name="messageText">Текст сообщения</param>
        public static void Add(MessageType messageType, string messageText)
        {
            Add(new LogMessage(messageType, messageText));
        }
        /// <summary>
        /// Добавить сообщение в лог
        /// </summary>
        /// <param name="message">сообщение</param>
        public static void Add(LogMessage message)
        {
            Messages.Add(message);

            if(WriteToConsole)
            {
                // Выбор цвета сообщения
                ConsoleColor messageColor = ConsoleColor.White;
                switch(message.Type)
                {
                    case MessageType.Info:
                        messageColor = ConsoleColor.Blue;
                        break;
                    case MessageType.Warning:
                        messageColor = ConsoleColor.Yellow;
                        break;
                    case MessageType.Error:
                        messageColor = ConsoleColor.Red;
                        break;
                }

                // Вывод сообщения
                Console.ForegroundColor = messageColor;
                Console.WriteLine(message.ToString());
                Console.ResetColor();
            }    
        }

        /// <summary>
        /// Сохранить лог в файл
        /// </summary>
        /// <param name="path">Путь по которому будет находиться файл лога</param>
        /// <param name="onlyErrors">Сохранять ли только ошибки</param>
        public static void Save(string path, bool onlyErrors = false)
        {
            StreamWriter fileWriter = new StreamWriter(path);
            foreach(LogMessage message in Messages)
            {
                // Если записывать весь лог
                if(!onlyErrors)
                    fileWriter.WriteLine(message.ToString());
                // Если записывать только ошибки
                else if(onlyErrors && message.Type == MessageType.Error)
                    fileWriter.WriteLine(message.ToString());
            }
            fileWriter.Close();
        }
    }
}
