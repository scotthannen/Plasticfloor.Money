using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plasticfloor.Money.Tests
{
    [TestClass]
    public class BinarySerialization
    {
        [TestMethod]
        public void BinarySerializesAndDeserializes()
        {
            var original = Currency.USD.Amount(1.11111m);
            var bytes = Serialize(original);
            var deserialized = Deserialize(bytes);
            Assert.AreEqual(original, deserialized);
        }

        private byte[] Serialize(Money money)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, money);
                return stream.ToArray();
            }
        }

        private Money Deserialize(byte[] serialized)
        {
            using (var stream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                stream.Write(serialized, 0, serialized.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return (Money)binForm.Deserialize(stream);
            }
        }
    }
}