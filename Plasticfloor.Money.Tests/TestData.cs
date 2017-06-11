namespace Plasticfloor.Money.Tests
{
    public static class Usd
    {
        public static Money Amount(decimal amount)
        {
            return Currencies.USD.Amount(amount);
        }
    }
}
