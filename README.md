# Plasticfloor.Money
Plasticfloor.Money is currency and amount, and nothing else. It doesn't convert currencies. It doesn't format because it doesn't know how you want your currency to look. Those are worthy causes to address in other projects, but not in this one. This is an abstraction of money.

Download the [NuGet package](https://www.nuget.org/packages/Plasticfloor.Money).

```csharp
var money = new Money(5.05M, Currency.HRK);
var moreMoney = money + .1050001M;
var rounded = Money.Round(moreMoney, 2);
var roundedHowYouWantIt = Money.Round(moreMoney, 2, MidpointRounding.AwayFromZero);
decimal amount = money.Amount;
decimal implicitlyConverted = money;
var currency = money.Currency;
```

- It's immutable.
- You can serialize it.
- There's a `MoneyDto` if for any reason you can't pass an `enum`.
- I'll add classes to handle formatting but I'm inclined to keep that separate. 
