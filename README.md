# Plasticfloor.Money
Plasticfloor.Money is currency and amount, and nothing else. It doesn't convert currencies. It doesn't format because it doesn't know how you want your money to look. Those are worthy causes to address in other projects, but not in this one. This is an abstraction of money.

Download the [NuGet package](https://www.nuget.org/packages/Plasticfloor.Money).
Provide input on [codereview.stackexchange.com](https://codereview.stackexchange.com/questions/165716/struct-for-money-and-currencies) or [e-mail me directly](mailto:scotthannen@gmail.com).

```csharp
var money = new Money(5.05M, Currency.HRK);
var moreMoney = money + .1050001M;
var rounded = Money.Round(moreMoney, 2);
var roundedHowYouWantIt = Money.Round(moreMoney, 2, MidpointRounding.AwayFromZero);
decimal amount = money.Amount;
var notAllowed = new Money(1, Currency.EUR) + new Money(1, Currency.USD); // CurrencyMismatchException
```

- It's immutable.
- You can serialize it.
- There's a `MoneyDto` if for any reason you can't transfer a `struct` containing an `enum`.
- I'll add classes to handle formatting but I'm inclined to keep that separate. 
