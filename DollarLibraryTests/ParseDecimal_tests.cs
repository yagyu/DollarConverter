using System;
using NUnit.Framework;
using WcfDollarLibrary;

namespace DollarLibraryTests
{
    [TestFixture]
    class ParseDecimal_Tests
    {
        [TestCase(0,0,"0")]
        [TestCase(0, 10, "0,1")]
        [TestCase(0, 10, "0,10")]
        [TestCase(0, 10, "0,10956")]
        [TestCase(25, 10, "25,1")]
        [TestCase(141, 33, "141,33")]
        [TestCase(334, 0, "334")]
        [TestCase(999999999, 99, "999999999,99")]
        [TestCase(999999999, 99, "999 999 999,99")]
        [TestCase(999999999, 99, "999.999.999,99")]
        public void ParseDecimal_Standard(int ones, int fractional, string value)
        {
            Assert.AreEqual(new Tuple<int, int>(ones, fractional), ParsingUtils.ParseDecimal(value));
        }

        public void ParseDecimal_Null()
        {
            Assert.Throws<ArgumentNullException>(delegate { ParsingUtils.ParseDecimal(null); });
        }

        [Test]
        public void ParseDecimal_EmptyString()
        {
            Assert.Throws<FormatException>(delegate { ParsingUtils.ParseDecimal(""); });
        }

        [TestCase("asdasfef")]
        [TestCase("         ")]
        [TestCase("......")]
        [TestCase("as123")]
        [TestCase("342kljf")]
        [TestCase("asdasf342553ef")]
        [TestCase("asdasf342,553ef")]
        [TestCase("3425,53ef")]
        [TestCase("999,822,88")]
        [TestCase("78, 03")]
        public void ParseDecimal_WrongString(string value)
        {
            Assert.Throws<FormatException>(delegate { ParsingUtils.ParseDecimal(value); });
        }
    }
}
