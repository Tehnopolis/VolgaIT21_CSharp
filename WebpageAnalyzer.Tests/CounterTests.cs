using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using WebpageAnalyzer.Components;
namespace WebpageAnalyzer.Tests
{
    [TestClass]
    public class CounterTests
    {
        [TestMethod]
        public void TestCaseSensetive()
        {
            Counter counter = new Counter();
            counter.CaseSensetive = true;

            // Ожидаемый результат: { { "Слово3", 3 }, { "Слово1", 2 }, { "Слово2", 2 }, { "слово2", 1 } }
            Dictionary<string,int> result = counter.Count(new string[] { "Слово1", "Слово1", "Слово2", "Слово3", "Слово2", "Слово3", "Слово3", "слово2" });

            // Всего 4 слова в тексте
            Assert.AreEqual(4, result.Count);

            // Проверить слова
            Assert.AreEqual(3, result["Слово3"]);
            Assert.AreEqual(2, result["Слово1"]);
            Assert.AreEqual(2, result["Слово2"]);
            Assert.AreEqual(1, result["слово2"]);
        }

        [TestMethod]
        public void TestNonCaseSensetive()
        {
            Counter counter = new Counter();

            // Ожидаемый результат: { { "В", 2 }, { "А", 1 }, { "Б", 1 } }
            Dictionary<string, int> result = counter.Count(new string[] { "а", "б", "в", "В" });

            // Всего 3 слова в тексте
            Assert.AreEqual(3, result.Count);

            // Проверить слова
            Assert.AreEqual(2, result["В"]);
            Assert.AreEqual(1, result["А"]);
            Assert.AreEqual(1, result["Б"]);
        }
    }
}
