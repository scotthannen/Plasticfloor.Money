using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Plasticfloor.Money
{
    /// <summary>
    /// Represents an amount of money in a specific currency.
    /// </summary>
    [Serializable]
    public struct Money : IComparable<Money>, IComparable, IXmlSerializable
    {
        /// <summary>
        /// Initializes a new instance of Money with the specified amount and currency.
        /// </summary>
        /// <param name="amount">The amount of money.</param>
        /// <param name="currency">The currency to represent with the instance of Money.</param>
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        /// <summary>
        /// The amount of money.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// The currency of the money.
        /// </summary>
        public Currency Currency { get; private set; }

        /// <summary>
        /// Returns a new instance of Money with the same currency as this instance and the specified amount.
        /// </summary>
        /// <param name="amount">The amount of money for the new instance.</param>
        /// <returns>A Money value with the same currency as this instance and the specified amount.</returns>
        public Money ToAmount(decimal amount) => new Money(amount, Currency);

        #region Math

        /// <summary>
        /// Adds a Decimal value to a Money value.
        /// </summary>
        /// <param name="d">The Decimal value to add.</param>
        /// <param name="m">The Money value to add.</param>
        /// <returns>A Money value with an amount equal to the d plus the amount of m.</returns>
        public static Money operator +(decimal d, Money m)
        {
            return new Money(m.Amount + d, m.Currency);
        }

        /// <summary>
        ///  Adds a Money value to a Decimal value.
        /// </summary>
        /// <param name="m">The Money value to add.</param>
        /// <param name="d">The Decimal value to add.</param>
        /// <returns>A Money value with an amount equal to the amount of m plus d.</returns>
        public static Money operator +(Money m, decimal d)
        {
            return d + m;
        }

        /// <summary>
        /// Adds two Money values of the same currency.
        /// </summary>
        /// <param name="m1">The first Money value to add.</param>
        /// <param name="m2">The second Money value to add.</param>
        /// <returns>A Money value equal to the sum of both Money values.</returns>
        /// <exception cref="CurrencyMismatchException">m1 and m2 represent different currencies.</exception>
        public static Money operator +(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1 + m2.Amount;
        }

        /// <summary>
        /// Subtracts a Decimal value from a Money value.
        /// </summary>
        /// <param name="m">The Money value from which to subtract.</param>
        /// <param name="d">The Decimal value to subtract.</param>
        /// <returns>A Money value with an amount equal to the amount of m minus d.</returns>
        public static Money operator -(Money m, decimal d)
        {
            return new Money(m.Amount - d, m.Currency);
        }

        /// <summary>
        /// Subtracts one Money value from another.
        /// </summary>
        /// <param name="m1">The Money value from which to subtract.</param>
        /// <param name="m2">The Money value to subtract.</param>
        /// <returns>A Money value with an amount equal to the amount of m1 minus the amount of m2.</returns>
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

        /// <summary>
        /// Adds a Decimal value to this value.
        /// </summary>
        /// <param name="d">The Decimal value to add.</param>
        /// <returns>A Money value with an amount equal to the amount of this instance plus d.</returns>
        public Money Add(decimal d) => this + d;

        /// <summary>
        /// Subtracts a Decimal value from this value.
        /// </summary>
        /// <param name="d">The Decimal value to subtract.</param>
        /// <returns>A Money value with an amount equal to the amount of this instance minus d.</returns>
        public Money Subtract(decimal d) => this - d;

        /// <summary>
        /// Multiples this value by a Decimal value.
        /// </summary>
        /// <param name="d">The multiplier.</param>
        /// <returns>A Money value with an amount equal to the amount of this instance multiplied by d.</returns>
        public Money Multiply(decimal d) => this * d;

        /// <summary>
        /// Divides this value by a Decimal value.
        /// </summary>
        /// <param name="d">The divisor.</param>
        /// <returns>A Money value with an amount equal to the amount of this instance divided by d.</returns>
        public Money Divide(decimal d) => this / d;

        /// <summary>
        /// Adds a Money value of the same currency to this value.
        /// </summary>
        /// <param name="m">The Money value to add.</param>
        /// <returns>A Money value equal to the amount of this instance plus the amount of m.</returns>
        public Money Add(Money m)
        {
            return this + m;
        }

        /// <summary>
        /// Subtracts a Money value of the same currency from this value.
        /// </summary>
        /// <param name="m">The Decimal value to subtract.</param>
        /// <returns>A Money value with an amount equal to the amount of this instance minus d.</returns>
        public Money Subtract(Money m)
        {
            return this - m;
        }

        /// <summary>
        /// Rounds a Money value to the nearest integer.
        /// </summary>
        /// <param name="m">The Money value to round.</param>
        /// <returns>The integer value of Money that is nearest to the d parameter. If d is halfway between two integers, one of which is even and the other odd, the even value is returned.</returns>
        /// <seealso cref="decimal.Round(Decimal)"/>
        public static Money Round(Money m)
        {
            return new Money(decimal.Round(m.Amount), m.Currency);
        }

        /// <summary>
        /// Rounds a Money value to a specified number of decimal places.
        /// </summary>
        /// <param name="m">The Money value to round.</param>
        /// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
        /// <returns>The Money value equivalent to m rounded to decimals number of decimal places.</returns>
        /// <seealso cref="decimal.Round(Decimal, Int32)"/>
        public static Money Round(Money m, int decimals)
        {
            return new Money(decimal.Round(m.Amount, decimals), m.Currency);
        }

        /// <summary>
        /// Rounds a Money value to the nearest integer. A parameter specifies how to round the value if it is midway between two other numbers.
        /// </summary>
        /// <param name="m">The Money value to round.</param>
        /// <param name="mode">A value that specifies how to round the amount of m if it is midway between two other numbers.</param>
        /// <returns>The integer value of Money that is nearest to the d parameter. If the amount of m is halfway between two numbers, one of which is even and the other odd, the mode parameter determines which of the two values is returned.</returns>
        /// <seealso cref="decimal.Round(Decimal, MidpointRounding)"/>
        public static Money Round(Money m, MidpointRounding mode)
        {
            return new Money(decimal.Round(m.Amount, mode), m.Currency);
        }

        /// <summary>
        /// Rounds a Money value to a specified precision. A parameter specifies how to round the value if it is midway between two other numbers.
        /// </summary>
        /// <param name="m">The Money value to round.</param>
        /// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
        /// <param name="mode">A value that specifies how to round the amount of m if it is midway between two other numbers.</param>
        /// <returns>The Money value that is nearest to the d parameter with a precision equal to the decimals parameter. If the amount of m is halfway between two numbers, one of which is even and the other odd, the mode parameter determines which of the two values is returned. If the precision of the amount of m is less than decimals, m is returned unchanged.</returns>
        /// <seealso cref="decimal.Round(Decimal, Int32, MidpointRounding)"/>
        public static Money Round(Money m, int decimals, MidpointRounding mode)
        {
            return new Money(decimal.Round(m.Amount, decimals, mode), m.Currency);
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

        /// <summary>
        /// Compares this instance to another <see cref="Money">Money</see> value of the same currency and returns an indication of their relative values. 
        /// </summary>
        /// <param name="value">The Money value to compare to this instance.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(Money value)
        {
            RequireSameCurrency(this, value);
            return Amount.CompareTo(value.Amount);
        }

        /// <summary>
        /// Compares this instance to another object and returns an indication of their relative values. 
        /// </summary>
        /// <param name="value">The object to compare to this instance, or <c>null</c>.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value is Money) return CompareTo((Money)value);
            throw new ArgumentException($"Cannot compare type {value.GetType()} to type Money.");
        }

        public static bool operator == (Money m1, Money m2)
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

        /// <summary>
        /// Compares this instance to a decimal value and returns an indication of their relative values. 
        /// </summary>
        /// <param name="value">The decimal value to compare to this instance.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(decimal value)
        {
            return Amount.CompareTo(value);
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

        private static void RequireSameCurrency(Money m1, Money m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
                throw new CurrencyMismatchException(m1.Currency, m2.Currency);
        }

        /// <summary>
        /// Returns a string representation of the Money value consisting of the Amount and the currency.
        /// </summary>
        /// <returns>A string representation of the Money value consisting of the Amount and the currency.</returns>
        /// <remarks>The string returned is not intended for UI display.</remarks>
        public override string ToString()
        {
            return string.Concat(Amount.ToString(CultureInfo.CurrentUICulture), " ", Currency.ToString());
        }
    }
}
