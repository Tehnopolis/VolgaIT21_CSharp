using Microsoft.VisualStudio.TestTools.UnitTesting;

using WebpageAnalyzer.Components;
namespace WebpageAnalyzer.Tests
{
    [TestClass]
    public class CleanerTests
    {
        [TestMethod]
        public void TestHtmlCleaner()
        {
            // Фильтр: убирает HTML теги и символы
            HtmlCleaner cleaner = new HtmlCleaner();

            // Ожидаемый результат: { "Слово", "Слово", "Слово" }
            string[] result = cleaner.Clean(new string[] { "Слово", "Слово</div>", "<p>Слово</p>" });

            // Фильтр прошло 3 слова?
            Assert.AreEqual(3, result.Length);

            // Проверить слова
            Assert.AreEqual("Слово", result[0]);
            Assert.AreEqual("Слово", result[1]);
            Assert.AreEqual("Слово", result[2]);
        }
    }
}
