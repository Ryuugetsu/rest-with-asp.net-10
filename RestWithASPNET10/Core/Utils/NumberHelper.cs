using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class NumberHelper
    {
        public static decimal ConvertToDecimal(string value)
        {
            decimal.TryParse(value,
                             System.Globalization.NumberStyles.Any,
                             System.Globalization.NumberFormatInfo.InvariantInfo,
                             out decimal number);
            return number;
        }

        public static bool IsNumeric(string value)
        {
            return decimal.TryParse(value,
                                    System.Globalization.NumberStyles.Any,
                                    System.Globalization.NumberFormatInfo.InvariantInfo,
                                    out decimal number);
        }
    }
}
