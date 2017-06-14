using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Plasticfloor.Money.Tests
{
    [TestClass]
    public class JsonSerialization
    {
        [TestMethod]
        public void JsonSerializesAndDeserializes()
        {
            var original = Currencies.AED.Amount(3.45678m);
            var serialized = JsonConvert.SerializeObject(original);
            var deserialized = JsonConvert.DeserializeObject<Money>(serialized);
            Assert.AreEqual(original, deserialized);
        }

        /// <summary>
        /// An earlier experiment with binary serialization caused
        /// JSON serialization to abbreviate property names. 
        /// I can't really control JSON serializers but I don't want
        /// to go out of my way to mess them up either.
        /// </summary>
        [TestMethod]
        public void JsonContainsPropertyNames()
        {
            var original = Currencies.AED.Amount(3.45678m);
            var serialized = JsonConvert.SerializeObject(original);
            Assert.IsTrue(serialized.IndexOf("amount", StringComparison.OrdinalIgnoreCase) > -1);
            Assert.IsTrue(serialized.IndexOf("currency", StringComparison.OrdinalIgnoreCase) > -1);
        }
    }
}