using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WebpageAnalyzer.Components;
namespace WebpageAnalyzer.Tests
{
    [TestClass]
    public class SplitterTests
    {
        [TestMethod]
        public void TestOneSplitterChar()
        {
            List<char> mySplitters = new List<char>() { ' ' };

            Splitter splitter = new Splitter(mySplitters);
            string[] result = splitter.Split("СловоX СловоY СловоZ");

            // Текст разбился на 3 слова ?
            Assert.AreEqual(3, result.Length);

            // Проверить слова
            Assert.AreEqual("СловоX", result[0]);
            Assert.AreEqual("СловоY", result[1]);
            Assert.AreEqual("СловоZ", result[2]);
        }

        [TestMethod]
        public void TestMultipleSplitterChars()
        {
            List<char> mySplitters = new List<char>() { ' ', '.', ',' };

            Splitter splitter = new Splitter(mySplitters);
            string[] result = splitter.Split("Слово1 Слово2.Слово3,Слово4");

            // Текст разбился на 4 слова ?
            Assert.AreEqual(4, result.Length);

            // Проверить слова
            Assert.AreEqual("Слово1", result[0]);
            Assert.AreEqual("Слово2", result[1]);
            Assert.AreEqual("Слово3", result[2]);
            Assert.AreEqual("Слово4", result[3]);
        }
    }
}
