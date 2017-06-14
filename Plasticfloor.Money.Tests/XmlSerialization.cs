using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plasticfloor.Money.Tests
{
    [TestClass]
    public class XmlSerialization
    {
        private readonly XmlSerializer _xmlSerializer = new XmlSerializer(typeof(Money));

        [TestMethod]
        public void XmlSerializesAndDeserializes()
        {
            var original = Currencies.USD.Amount(1.11111m);
            var xml = SerializeXml(original);
            var deserialized = DeserializeXml(xml);
            Assert.AreEqual(original, deserialized);
        }

        [TestMethod]
        public void InvalidCurrencyDeserializesToDefaults()
        {
            var xml = SerializeXml(Currencies.EUR.Amount(5)).Replace("EUR", "XYZ");
            var deserialized = DeserializeXml(xml);
            Assert.IsTrue(xml.Contains("XYZ"));
            Assert.AreEqual(deserialized, Currencies.Unknown.Amount(0));
        }

        private string SerializeXml(Money money)
        {
            using (var writer = new StringWriter())
            {
                _xmlSerializer.Serialize(writer, money);
                return writer.ToString();
            }
        }

        private Money DeserializeXml(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                return (Money) _xmlSerializer.Deserialize(reader);
            }
        }
    }
}
