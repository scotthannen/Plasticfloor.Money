using System;

namespace Plasticfloor.Money
{
    public static class Extensions
    {
        public static MoneyDto ToDto(this Money input)
        {
            return new MoneyDto(input.Amount, (int)input.Currency);
        }

        public static Money ToMoney(this MoneyDto input)
        {
            return new Money(input.Amount, (Currency)input.CurrencyCode);
        }

        public static Money Amount(this Currency input, decimal amount)
        {
            return new Money(amount, input);
        }
    }
}