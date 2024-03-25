using System.Globalization;
using System.Text.RegularExpressions;

namespace uk.co.nfocus.fathima.project.Support
{
    internal static class ConversionHelper
    {
        //Converts a string representation of a decimal
        //number with currency symbol to a decimal value.
        public static decimal ConvertStringToDecimal(string myString)
        {
            NumberStyles style = NumberStyles.AllowCurrencySymbol | NumberStyles.Number;
            CultureInfo provider = new CultureInfo("en-GB");
            return decimal.Parse(myString, style, provider);
        }

        //Extracts digits from the input string and
        //converts them to an integer.
        public static int ConvertStringToInt(string myString)
        {
            // Use Regex.Replace to remove non-digit characters from the input string
            string digitsOnly = Regex.Replace(myString, "[^0-9]", "");
            // Parse the cleaned string into an integer
            return int.Parse(digitsOnly);
        }
    }
}
