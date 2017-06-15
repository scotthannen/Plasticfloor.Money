using System;

namespace Plasticfloor.Money
{
    public class CurrencyMismatchException : Exception
    {
        internal CurrencyMismatchException(Currency c1, Currency c2)
            : base($"This operation cannot be performed between {c1} and {c2}.")
        { }
    }
}