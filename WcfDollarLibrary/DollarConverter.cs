﻿using System;
using System.ServiceModel;
using System.Text;


namespace WcfDollarLibrary
{
    public class DollarConverter : IDollar
    {
        private static readonly string[] currencies = { "dollar", "cent" };
        private static readonly string pluralSuffix = "s";
        /// <summary>
        /// Main method of the WCF service
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Convert(string value)
        {
            try
            {
                return ConvertCurrency(value);
            }
            catch(ArgumentNullException e)
            {
                throw new FaultException<ArgumentNullException>(e, new FaultReason("Argument is null"));
            }
            catch (FormatException e)
            {
                throw new FaultException<FormatException>(e,new FaultReason (e.Message));
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new FaultException<ArgumentOutOfRangeException>(e, new FaultReason("Out of range"));
            }
        }

        /// <summary>
        /// Converts string representing decimal number into dollars and cents
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertCurrency(string value)
        {

            Tuple<int, int> t = ParsingUtils.ParseDecimal(value); //no check for exceptions, apropiate logging was added in ParsingUtils and exceptions will be converted into WCF faults elsewhere 
            StringBuilder result = new StringBuilder();
            result.Append($"{ParsingUtils.NumeralsAsWords(t.Item1)} {currencies[0]}");
            if(t.Item1!=1)
            {
                result.Append(pluralSuffix);
            }
            if(t.Item2>0)
            {
                result.Append($" {ParsingUtils.NumeralsAsWords(t.Item2)} {currencies[1]}");
                if (t.Item2 != 1)
                {
                    result.Append(pluralSuffix);
                }
            }
            return result.ToString();
        }
    }


}
