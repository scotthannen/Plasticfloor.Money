using System.IO;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plasticfloor.Money.Tests
{
    [TestClass]
    public class DataContractSerialization
    {
        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(Money));

        [TestMethod]
        public void DataContractSerializesAndDeserializes()
        {
            var original = Currencies.USD.Amount(1.11111m);
            var bytes = Serialize(original);
            var deserialized = Deserialize(bytes);
            Assert.AreEqual(original, deserialized);
        }

        private byte[] Serialize(Money money)
        {
            using (var stream = new MemoryStream())
            {
                _serializer.WriteObject(stream, money);
                return stream.ToArray();
            }
        }

        private Money Deserialize(byte[] serialized)
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(serialized, 0, serialized.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return (Money)_serializer.ReadObject(stream);
            }
        }
    }
}