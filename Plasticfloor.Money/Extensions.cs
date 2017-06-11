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
            return new Money(input.Amount, (Currencies)input.CurrencyCode);
        }

        public static Money Amount(this Currencies input, decimal amount)
        {
            return new Money(amount, input);
        }

        public static Money AddMargin(this Money input, decimal margin)
        {
            if(!(input.Amount < 1 && input.Amount >-1))
                throw new ArgumentOutOfRangeException(nameof(margin), "Margin must be greater than -1 and less than 1.");
            return new Money(input.Amount /(1-margin), input.Currency);
        }
    }
}