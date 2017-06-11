using System;

namespace Plasticfloor.Money
{
    public class CurrencyMismatchException : Exception
    {
        internal CurrencyMismatchException(Currencies c1, Currencies c2)
            : base($"This operation cannot be performed between {c1} and {c2}.")
        { }
    }
}