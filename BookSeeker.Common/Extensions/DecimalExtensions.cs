using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BookSeeker.Common.Extensions
{
    public static class DecimalExtension
    {
        private static readonly Dictionary<string, CultureInfo> ISOCurrenciesToCultureMap =
            CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new
                {
                    currency = c,
                    new RegionInfo(c.LCID).ISOCurrencySymbol
                })
                .GroupBy(x => x.ISOCurrencySymbol)
                .ToDictionary(g => g.Key, g => g.First().currency, StringComparer.OrdinalIgnoreCase);

        public static string FormatCurrency(this decimal amount, string currencyCode)
        {
            return ISOCurrenciesToCultureMap.TryGetValue(currencyCode, out var culture)
                ? string.Format(culture, "{0:C}", amount)
                : amount.ToString("0.00");
        }
    }
}