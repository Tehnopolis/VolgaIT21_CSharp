using System;

namespace WebpageAnalyzer.Log
{
    /// <summary>
    /// Тип сообщения
    /// </summary>
    public enum MessageType
    {
        Info,
        Warning,
        Error
    }

    /// <summary>
    /// Данные сообщения лога
    /// </summary>
    public struct LogMessage
    {
        /// <summary>
        /// Дата и время сообщения
        /// </summary>
        public DateTime Date { get; }
        /// <summary>
        /// Тип сообщения (Информация/Предупреждение/Ошибка)
        /// </summary>
        public MessageType Type { get; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; }

        public LogMessage(MessageType type, string text)
        {
            this.Date = DateTime.Now;
            this.Type = type;
            this.Text = text;
        }

        public override string ToString()
        {
            //Формат: [Дата Время][ТИП]: Текст
            return $"[{this.Date.ToString("dd/MM/yyyy HH:mm")}][{this.Type.ToString().ToUpper()}]: {this.Text}";
        }
    }
}
