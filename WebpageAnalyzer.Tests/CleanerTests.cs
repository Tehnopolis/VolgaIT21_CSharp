using Microsoft.VisualStudio.TestTools.UnitTesting;

using WebpageAnalyzer.Tools;
namespace WebpageAnalyzer.Tests
{
    [TestClass]
    public class CleanerTests
    {
        [TestMethod]
        public void TestSimpleCleaner()
        {
            // Фильтр: пропускает только слова, которые начинаются на _
            Cleaner cleaner = new Cleaner((word) => word.StartsWith('_'));

            // Ожидаемый результат: { "_Слово1", "_Слово2_" }
            string[] result = cleaner.Clean(new string[] { "Слово0", "_Слово1", "_Слово2_" });

            // Фильтр прошло 2 слова?
            Assert.AreEqual(2, result.Length);

            // Проверить слова
            Assert.AreEqual("_Слово1", result[0]);
            Assert.AreEqual("_Слово2_", result[1]);
        }

        [TestMethod]
        public void TestHtmlCleaner()
        {
            // Фильтр: пропускает только слова, которые не начинаются на < и не заканчиваются на >
            Cleaner cleaner = new Cleaner((word) => !word.StartsWith('<') && !word.EndsWith('>'));

            // Ожидаемый результат: { "Слово1", "Слово2" }
            string[] result = cleaner.Clean(new string[] { "<Слово0>", "Слово1", "Слово2", "<Слово3/>" });

            // Фильтр прошло 2 слова?
            Assert.AreEqual(2, result.Length);

            // Проверить слова
            Assert.AreEqual("Слово1", result[0]);
            Assert.AreEqual("Слово2", result[1]);
        }
    }
}
