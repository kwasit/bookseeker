using System.Globalization;

namespace BookSeeker.Common.Extensions
{
    public static class CultureInfoExtensions
    {
        public static string GetCurrencyCode(this CultureInfo cultureInfo) => new RegionInfo(cultureInfo.LCID).ISOCurrencySymbol;
    }
}