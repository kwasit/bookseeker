using BookSeeker.Common.Extensions;
using BookSeeker.Engine.Models;

namespace BookSeeker.Engine.Extensions
{
    public static class MoneyExtensions
    {
        public static string FormatCurrency(this Money money) => money.Amount.FormatCurrency(money.CurrencyCode);
    }
}