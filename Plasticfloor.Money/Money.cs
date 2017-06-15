using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Plasticfloor.Money
{
    [Serializable]
    public struct Money : IComparable<Money>, IComparable, IXmlSerializable
    {

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        public Money ToAmount(decimal amount) => new Money(amount, Currency);

        #region Math

        public static Money operator +(decimal d, Money m)
        {
            return new Money(m.Amount + d, m.Currency);
        }

        public static Money operator +(Money m, decimal d)
        {
            return d + m;
        }

        public static Money operator +(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1 + m2.Amount;
        }

        public static Money operator -(decimal d, Money m)
        {
            return new Money(d - m.Amount, m.Currency);
        }

        public static Money operator -(Money m, decimal d)
        {
            return new Money(m.Amount - d, m.Currency);
        }

        public static Money operator -(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1 - m2.Amount;
        }

        public static Money operator *(Money m, decimal d)
        {
            return new Money(m.Amount * d, m.Currency);
        }

        public static Money operator /(Money m1, decimal d)
        {
            return new Money(m1.Amount / d, m1.Currency);
        }

        public Money Add(decimal add) => this + add;

        public Money Subtract(decimal subtract) => this - subtract;

        public Money Multiply(decimal by) => this * by;

        public Money Divide(decimal by) => this / by;

        public Money Add(Money add)
        {
            return this + add;
        }

        public Money Subtract(Money subtract)
        {
            return this - subtract;
        }

        public static Money operator *(Money m, long l)
        {
            return new Money(m.Amount * l, m.Currency);
        }

        public static Money operator *(long l, Money m)
        {
            return m * l;
        }

        public static Money operator /(Money m1, long l)
        {
            return new Money(m1.Amount / l, m1.Currency);
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

        #endregion

        #region Comparison

        public bool Equals(Money other)
        {
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Money && Equals((Money)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Amount and Currency are not read-only but are only
                // set by the constructor or deserialization.
                return (Amount.GetHashCode() * 397) ^ (int)Currency;
            }
        }

        public int CompareTo(Money other)
        {
            RequireSameCurrency(this, other);
            return Amount.CompareTo(other.Amount);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (obj is Money) return CompareTo((Money)obj);
            throw new ArgumentException($"Cannot compare type {obj.GetType()} to type Money.");
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

        public int CompareTo(decimal d)
        {
            return Amount.CompareTo(d);
        }

        #endregion

        #region XmlSerialization

        private const string XmlAmountAttribute = "amount";
        private const string CurrencyAttribute = "currency";

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (decimal.TryParse(reader.GetAttribute(XmlAmountAttribute), out decimal amount)
                && Enum.TryParse(reader.GetAttribute(CurrencyAttribute), out Currency currency))
            {
                Amount = amount;
                Currency = currency;
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(XmlAmountAttribute, Amount.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString(CurrencyAttribute, Currency.ToString());
        }

        #endregion

        public static void RequireSameCurrency(Money m1, Money m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
                throw new CurrencyMismatchException(m1.Currency, m2.Currency);
        }

        /// <summary>
        /// Not intended for UI.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(Amount.ToString(CultureInfo.CurrentUICulture), " ", Currency.ToString());
        }
    }
}
