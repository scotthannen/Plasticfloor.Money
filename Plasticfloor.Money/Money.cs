using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plasticfloor.Money
{
    public struct Money : IComparable<Money>, IComparable
    {
        public bool Equals(Money other)
        {
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Money && Equals((Money) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Amount.GetHashCode() * 397) ^ (int) Currency;
            }
        }

        public Money(decimal amount, Currencies currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }

        public Currencies Currency { get; }

        public Money ToAmount(decimal amount) => new Money(amount, Currency);

        public Money Add(decimal add) => new Money(Amount + add, Currency);

        public Money Subtract(decimal subtract) => new Money(Amount - subtract, Currency);

        public Money Multiply(decimal by) => new Money(Amount * by, Currency);

        public Money Divide(decimal by) => new Money(Amount / by, Currency);

        public int CompareTo(Money other)
        {
            RequireSameCurrency(this, other);
            return Amount.CompareTo(other.Amount);
        }

        public int CompareTo(decimal d)
        {
            return Amount.CompareTo(d);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (obj is Money) return CompareTo((Money)obj);
            throw new ArgumentException($"Cannot compare type {obj.GetType()} to type Money.");
        }

        public static Money operator +(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return new Money(m1.Amount + m2.Amount, m1.Currency);
        }

        public static Money operator -(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return new Money(m1.Amount - m2.Amount, m1.Currency);
        }

        public static bool operator ==(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1.Amount.Equals(m2.Amount);
        }

        public static bool operator !=(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return !m1.Amount.Equals(m2.Amount);
        }

        public static bool operator >(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1.Amount > m2.Amount;
        }

        public static bool operator <(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1.Amount < m2.Amount;
        }

        public static bool operator >=(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1.Amount >= m2.Amount;
        }

        public static bool operator <=(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1.Amount <= m2.Amount;
        }

        public static bool operator >(Money m, decimal d)
        {
            return m.Amount > d;
        }

        public static bool operator <(Money m, decimal d)
        {
            return m.Amount < d;
        }

        public static bool operator >=(Money m, decimal d)
        {
            return m.Amount >= d;
        }

        public static bool operator <=(Money m, decimal d)
        {
            return m.Amount <= d;
        }

        public static Money operator +(Money m, decimal d)
        {
            return new Money(m.Amount + d, m.Currency);
        }

        public static Money operator -(Money m, decimal d)
        {
            return new Money(m.Amount - d, m.Currency);
        }

        public static Money operator +(decimal d, Money m)
        {
            return new Money(m.Amount + d, m.Currency);
        }

        public static Money operator -(decimal d, Money m)
        {
            return new Money(m.Amount - d, m.Currency);
        }

        public static Money operator *(Money m1, decimal d)
        {
            return new Money(m1.Amount * d, m1.Currency);
        }

        public static Money operator /(Money m1, decimal d)
        {
            return new Money(m1.Amount / d, m1.Currency);
        }

        public static Money operator *(Money m1, long l)
        {
            return new Money(m1.Amount * l, m1.Currency);
        }

        public static Money operator /(Money m1, long l)
        {
            return new Money(m1.Amount / l, m1.Currency);
        }

        public static void RequireSameCurrency(Money m1, Money m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
                throw new CurrencyMismatchException(m1.Currency, m2.Currency);
        }

        public override string ToString()
        {
            return string.Concat(Amount.ToString(CultureInfo.CurrentUICulture), " ", Currency.ToString());
        }

        public Money Round()
        {
            return new Money(decimal.Round(Amount), Currency);
        }

        public Money Round(int decimals)
        {
            return new Money(decimal.Round(Amount, decimals), Currency);
        }

        public Money Round(MidpointRounding mode)
        {
            return new Money(decimal.Round(Amount, mode), Currency);
        }

        public Money Round(int decimals, MidpointRounding mode)
        {
            return new Money(decimal.Round(Amount, decimals, mode), Currency);
        }
    }
}
