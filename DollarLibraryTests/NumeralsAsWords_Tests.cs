using System;
using NUnit.Framework;
using WcfDollarLibrary;

namespace DollarLibraryTests
{
    [TestFixture]
    class NumeralsAsWords_Tests
    {
        [TestCase(0,"zero")]
        [TestCase(1, "one")]
        [TestCase(2, "two")]
        [TestCase(3, "three")]
        [TestCase(4, "four")]
        [TestCase(5, "five")]
        [TestCase(6, "six")]
        [TestCase(7, "seven")]
        [TestCase(8, "eight")]
        [TestCase(9, "nine")]
        [TestCase(10, "ten")]
        [TestCase(11, "eleven")]
        [TestCase(12, "twelve")]
        [TestCase(13, "thirteen")]
        [TestCase(14, "fourteen")]
        [TestCase(15, "fifteen")]
        [TestCase(16, "sixteen")]
        [TestCase(17, "seventeen")]
        [TestCase(18, "eighteen")]
        [TestCase(19, "nineteen")]
        [TestCase(20, "twenty")]
        [TestCase(30, "thirty")]
        [TestCase(40, "forty")]
        [TestCase(50, "fifty")]
        [TestCase(60, "sixty")]
        [TestCase(70, "seventy")]
        [TestCase(80, "eighty")]
        [TestCase(90, "ninety")]
        [TestCase(100, "one hundred")]
        [TestCase(1000, "one thousand")]
        [TestCase(25, "twenty-five")]
        [TestCase(121, "one hundred twenty-one")]
        [TestCase(1920, "nineteen hundred twenty")]
        [TestCase(999999999, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine")]
        [TestCase(2000000, "two million")]
        public void NumeralsAsWords_Standard(int value, string result)
        {
            Assert.AreEqual(result, ParsingUtils.NumeralsAsWords(value));
        }


        [TestCase(-1)]
        [TestCase(1000000000)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void NumeralsAsWords_OutOfRange(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(delegate { ParsingUtils.NumeralsAsWords(value); });
        }
    }
}
