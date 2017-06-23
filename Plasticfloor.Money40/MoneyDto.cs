using System;

namespace Plasticfloor.Money
{
    public struct MoneyDto
    {
        public MoneyDto(decimal amount, int currencyCode)
        {
            Amount = amount;
            CurrencyCode = currencyCode;
        }

        public decimal Amount { get; set; }
        public int CurrencyCode { get; set; }
    }
}