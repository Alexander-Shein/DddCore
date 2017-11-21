// ReSharper disable InconsistentNaming


using System.Collections.Generic;

namespace DddCore.BLL.Domain
{
    public enum Currency
    {
        AED = 784,
        AFN = 971
    }

    public static class CurrencyExtensions
    {
        public static string ThreeLetterIso(this Currency currency)
        {
            return currency.ToString("G");
        }

        public static string Symbol(this Currency currency)
        {
            return _symbolMap[currency];
        }

        public static int NumberOfDigitsAfterDecimalSeparator(this Currency currency)
        {
            return _numberOfDigitsAfterDecimalSeparatorMap[currency];
        }

        public static int IsoNumber(this Currency currency)
        {
            return (int)currency;
        }

        private const int TwoDigits = 2;
        private const int NoDigits = 0;

        private static readonly Dictionary<Currency, int> _numberOfDigitsAfterDecimalSeparatorMap = new Dictionary<Currency, int>
        {
            { Currency.AED, TwoDigits },
            { Currency.AFN, TwoDigits }
        };

        private static readonly Dictionary<Currency, string> _symbolMap = new Dictionary<Currency, string>
        {
            { Currency.AED, "د.إ" },
            { Currency.AFN, "؋" }
        };
    }
}
