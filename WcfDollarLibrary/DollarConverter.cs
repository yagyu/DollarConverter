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
            return string.Format($"{ParsingUtils.NumeralsAsWords(t.Item1)} dollars {ParsingUtils.NumeralsAsWords(t.Item2)} cents");
        }
    }


}
