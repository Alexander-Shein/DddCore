using System.Collections.Generic;

namespace DddCore.BLL.Domain.ValueObjects.Finances
{
    public class Money : ValueObjectBase<Money>
    {
        public Currency Currency { get; }
        public decimal Amount { get; }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return Currency;
            yield return Amount;
        }

        public Money(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public bool IsPositive => Amount > 0;
    }
}