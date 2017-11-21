using System.Collections.Generic;
using DddCore.BLL.Domain.ValueObjects;

namespace DddCore.BLL.Domain
{
    public class Money : ValueObjectBase<Money>
    {
        public Currency CurrencyIso3 { get; }
        public decimal Amount { get; }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return CurrencyIso3;
            yield return Amount;
        }

        public Money(Currency currencyIso3, decimal amount)
        {
            CurrencyIso3 = currencyIso3;
            Amount = amount;
        }
    }
}