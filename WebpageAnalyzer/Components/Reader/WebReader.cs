using System;
using System.IO;
using System.Net;

namespace WebpageAnalyzer.Components
{
    /// <summary>
    /// Помогает получить текст по указанному URL
    /// </summary>
    public sealed class WebReader : IReader
    {
        /// <summary>
        /// URL сайта, на который будет отправлен HTTP запрос
        /// </summary>
        public string URL { get; set; }

        public WebReader()
        { }

        public WebReader(string url)
        {
            this.URL = url;
        }

        /// <summary>
        /// Отправить HTTP запрос на сайт и получить текст
        /// </summary>
        /// <returns>Текст по указанному URL</returns>
        /// <exception cref="InvalidOperationException">Если URL не был определен</exception>
        public string Read()
        {
            if (this.URL == null || this.URL.Trim() == "")
                throw new InvalidOperationException("URL не был определен");

            // Отправить запрос
            WebRequest request = WebRequest.Create(this.URL);
            request.Method = "GET";

            // Прочитать ответ
            using (var webResponse = request.GetResponse())
                using (var webStream = webResponse.GetResponseStream())
                    using (var reader = new StreamReader(webStream))
                        return reader.ReadToEnd();
        }
    }
}
