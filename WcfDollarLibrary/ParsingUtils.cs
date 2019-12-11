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
        private static readonly string[] tensNames = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static readonly string[] teensNames = { "ten", "eleven", "twelve", "thirteen", "forteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static readonly string[] unitNames = { "hundred", "thousand", "million" };
        /// <summary>
        /// The method for converting digit to its numeric value
        /// uses an assumption that all the digits are between '0' and '9'
        /// ultra-correct alternative would be to do switch with case for every digit
        /// </summary>
        /// <param name="c">character for conversion</param>
        /// <returns></returns>
        /// <exception cref="FormatException">thrown when character isn't digit</exception>
        private static int CharToInt(char c)
        {
            if(!char.IsDigit(c))
            {
                throw new FormatException();
            }
            return (int)(c - '0'); 
        }

        /// <summary>
        /// Converting string to int
        /// ignoring common thousands separators, i. e.  ' ' and '.'
        /// for simplicity theres no check if separators are in correct places
        /// </summary>
        /// <param name="toParse"></param>
        /// <returns></returns>
        /// <exception cref="FormatException">Thrown when theres illegal character in toParse</exception>
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
                    Debug.WriteLine("Fails parsing with" + toParse);
                    throw new FormatException("Fails parsing with" + toParse); //adding the string for which the parsing had failed for easier debugging
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toParse"></param>
        /// <returns></returns>
        public static Tuple<int,int> ParseMoneyAmount(string toParse)
        {
            int decimals=0;
            int ones=0;
            if (toParse == null)
            {
                throw new ArgumentNullException();
            }
            if (toParse=="")
            {
                throw new FormatException("Empty string");
            }
            string[] values = toParse.Split(',');
            if (values.Length > 2 || values.Length == 0)
            { 
                Debug.WriteLine("Fails parsing with" + toParse);
            throw new FormatException("Fails parsing with" + toParse);
            }
            if (values.Length == 2)
            {
                string fractionText = values[1].TrimEnd(' ');
                if (fractionText.Length == 1) //user provided just tenths of a dollar
                {
                    fractionText +="0";

                }
                fractionText = fractionText.Substring(0, 2); //truncate too many digits
                if(!fractionText.All(char.IsDigit))
                {
                    Debug.WriteLine("Fails parsing with" + toParse);
                    throw new FormatException("Fails parsing with" + toParse);
                }
                try
                {
                    decimals = ParseInt(fractionText);
                }
                catch (FormatException)
                {
                    Debug.WriteLine("Fail parsing with" + toParse);
                    throw new FormatException("Fail parsing with" + toParse); //adding the string for which the parsing had failed for easier debugging
                }

               
            }
            //for values with length 1 cents are 0
            try
            {
                ones = ParseInt(values[0]);
            }
            catch (FormatException)
            {
                Debug.WriteLine("Fail parsing with" + toParse);
                throw new FormatException("Fail parsing with" + toParse); //adding the string for which the parsing had failed for easier debugging
            }
            return new Tuple<int, int>(ones, decimals);



        }

        /// <summary>
        /// Method for converting numbers 0-999999999 into words
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
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
            result.Append(tensNames[tens]);
            if(ones>0 && tens > 0)
            {
                result.Append("-");
            }

            if(ones>0)
            {
                result.Append(onesNames[ones]);
            }
            return result.ToString();
        }
    }
}

