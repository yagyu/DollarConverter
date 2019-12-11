using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WcfDollarLibrary;

namespace DollarLibraryTests
{
    [TestFixture]
    public class DollarTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2000)]
        [TestCase(999999999)]
        public void ParseInt_StandardPass(int value)
        {
            Assert.AreEqual(value, ParsingUtils.ParseInt(value.ToString()));
        }

        [TestCase(0,"0")]
        [TestCase(1,"1")]
        [TestCase(2000,"2000")]
        [TestCase(999999999, "999999999")]
        public void ParseInt_StandardPassStrings(int value, string numeral)
        {
            Assert.AreEqual(value, ParsingUtils.ParseInt(numeral));
        }

        [TestCase(1000, "1 000")]
        [TestCase(10000, "10 000")]
        [TestCase(1000000, "1 000 000")]
        [TestCase(2000, "2 000")]
        [TestCase(999999999, "999 999 999")]
        [TestCase(1000, "1.000")]
        [TestCase(10000, "10.000")]
        [TestCase(1000000, "1.000.000")]
        [TestCase(2000, "2.000")]
        [TestCase(999999999, "999.999.999")]
        public void ParseInt_StandardPassStringsThousandsSeparator(int value, string numeral)
        {
            Assert.AreEqual(value, ParsingUtils.ParseInt(numeral));
        }

    }
}
