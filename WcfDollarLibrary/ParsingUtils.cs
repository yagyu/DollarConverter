using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WcfDollarLibrary
{
    public static class ParsingUtils
    {
        private static readonly string[] onesNames = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static readonly string[] tensNames = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static readonly string[] teensNames = { "ten", "eleven", "twelve", "thirteen", "forteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static readonly string[] unitNames = { "hundred", "thousand", "million" };
        /// <summary>
        /// The method to convert digit to its numeric value
        /// uses an assumption that all the digits are between '0' and '9'
        /// ultra-correct alternative would be to do switch with case for every digit
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static int CharToInt(char c)
        {
            if(!char.IsDigit(c))
            {
                throw new FormatException();
            }
            return (int)(c - '0'); 
        }
        public static int ParseInt(string toParse)
        {
            int result = 0;
            string temp = toParse.Replace(" ", "").Replace(".", ""); //removing thousands separators (without strict check about their position);
            foreach(char c in temp)
            {
                result *= 10;
                try
                {
                    result += CharToInt(c);
                }
                catch(FormatException )
                {
                    Debug.WriteLine("Fail parsing with" + toParse);
                    throw new FormatException(toParse); //adding the string for which the parsing had failed for easier debugging
                }
            }
            return result;
        }
        public static Tuple<int,int> ParseMoneyAmount(string toParse)
        {
            int cents=0;
            int dollars=0;
            if (toParse == null)
            {
                throw new ArgumentNullException();
            }
            if (toParse=="")
            {
                throw new FormatException("");
            }
            string[] values = toParse.Split(',');
            if (values.Length > 2 || values.Length == 0)
            {
                throw new FormatException();
            }
            if (values.Length == 2)
            {
                string centText = values[1].TrimEnd(' ');
                if (centText.Length == 1) //user provided just tenths of a dollar
                {
                    centText.Append('0');
                }
                if(!centText.All(char.IsDigit))
                {
                    throw new FormatException(toParse);
                }
                try
                {
                    cents = ParseInt(centText);
                }
                catch (FormatException)
                {
                    Debug.WriteLine("Fail parsing with" + toParse);
                    throw new FormatException(toParse); //adding the string for which the parsing had failed for easier debugging
                }
                while (cents>=100)
                {
                    cents /= 10; //in case somebody entered too many digits after comma (just truncate, could be also rounding)
                }
               
            }
            //for values with length 1 cents are 0
            try
            {
                dollars = ParseInt(values[0]);
            }
            catch (FormatException)
            {
                Debug.WriteLine("Fail parsing with" + toParse);
                throw new FormatException(toParse); //adding the string for which the parsing had failed for easier debugging
            }
            return new Tuple<int, int>(dollars, cents);



        }

        public static string NumeralsAsWords(int number)
        {
            if (number >= 1000000000 || number < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (number == 0)
            {
                return onesNames[0];
            }

            int millions = number / 1000000;
            int thousands = (number % 1000000)/1000;
            int ones = (number % 1000);
            int hundreds = ones / 100;
            StringBuilder result = new StringBuilder("");
            if(millions>0)
            {
                result.Append($"{SmallNumerals(millions)} {unitNames[2]} ");
            }
            if(thousands>0)
            {
                if(thousands==1 && hundreds>0)
                {
                    result.Append($"{teensNames[hundreds]} {unitNames[0]} ");
                    ones %= 100;
                }
                else
                {
                    result.Append($"{SmallNumerals(thousands)} {unitNames[1]} ");
                }
            }
            result.Append(SmallNumerals(ones));
            return result.ToString();
        }

        /// <summary>
        /// Helper function for numbers less than 1000
        /// returning empty string for 0
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string SmallNumerals(int number)
        {

            if(number>=1000 || number<0)
            {
                throw new ArgumentOutOfRangeException();
            }
            int hundreds = number / 100;
            int tens = (number % 100) / 10;
            int ones = number % 10;
            StringBuilder result = new StringBuilder();
            if(number>100)
            {
                
                if (hundreds>0)
                {
                    result.Append(onesNames[hundreds]);
                    result.Append(" ");
                    result.Append(unitNames[0]);
                    result.Append(" ");
                }

            }

            if(tens==1) //special case for 10-19
            {
                result.Append(teensNames[ones]);
                return result.ToString();
            }
            if (tens>0)
            {
                result.Append(tensNames[tens]);
                if(ones>0)
                {
                    result.Append("-");
                }
            }
            if(ones>0)
            {
                result.Append(onesNames[ones]);
            }
            return result.ToString();
        }
    }
}

