using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace WcfDollarLibrary
{
    public class DollarConverter : IDollar
    {
        private static readonly string[] currencies = { "dollar", "cent" };
        private static readonly string pluralSuffix = "s";
        public string Convert(string value)
        {

            return ConvertDollars(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertDollars(string value)
        {
            Tuple<int, int> t = ParsingUtils.ParseMoneyAmount(value);
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
