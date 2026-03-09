using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiDoChopp.Domain.Utils.Extensions
{
    public static class PrintExtensions
    {
        public static string FormatTextSize(this string text, int maxLength)
        {
            if (text.Length >= maxLength)
                return text.Substring(0, maxLength);

            return text.PadRight(maxLength);
        }

        public static string ToBrazilianCurrency(this double value)
        {
            return value.ToString("N2", new System.Globalization.CultureInfo("pt-BR"));
        }
    }
}
