using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.fathima.project.Support
{
    internal static class ConversionHelper
    {
        public static decimal StringToDecimal(string myString)
        {
            NumberStyles style = NumberStyles.AllowCurrencySymbol | NumberStyles.Number;
            CultureInfo provider = new CultureInfo("en-GB");
            return decimal.Parse(myString, style, provider);
        }
    }
}
