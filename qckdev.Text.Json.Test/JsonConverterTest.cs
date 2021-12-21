using Microsoft.VisualStudio.TestTools.UnitTesting;
using qckdev.Text.Json.Test.TestObjects;
using System;

namespace qckdev.Text.Json.Test
{
    [TestClass]
    public class JsonConverterTest
    {

        [TestMethod]
        public void SerializeAndDeserialize_Temperature()
        {
            var expected = new Temperature()
            {
                Date = DateTime.Parse("2019-08-01T00:00:00-07:00"),
                TemperatureCelsius = 25,
                Summary = "Hot"
            };
            var json = JsonConvert.SerializeObject(expected);
            var value = JsonConvert.DeserializeObject<Temperature>(json);

            Assert.AreEqual(
                new { expected.Date, expected.TemperatureCelsius, expected.Summary },
                new { value.Date, value.TemperatureCelsius, value.Summary }
            );
        }

        [TestMethod]
        public void Deserialize_Pokemon()
        {
            var rdo = JsonConvert.DeserializeObject<Pokemon>(@"{""id"":132, ""name"":""ditto"", ""order"":203, ""weight"":40, ""species"": {""name"":""ditto"", ""url"":""https://pokeapi.co/api/v2/pokemon-species/132/""} }");

            Assert.AreEqual(
                new { Id = 132, Name = "ditto", Order = 203, Spices = new { Name = "ditto", Url = "https://pokeapi.co/api/v2/pokemon-species/132/" } },
                new { rdo.Id, rdo.Name, rdo.Order, Spices = new { rdo.Species.Name, rdo.Species.Url } }
            );
        }

        [TestMethod]
        public void Deserialize_Dynamic()
        {
            dynamic value = JsonConvert.DeserializeObject(@"{""id"":132, ""name"":""ditto"", ""order"":203, ""weight"":40 }");

            Assert.AreEqual(
                new { Id = 132, Name = "ditto", Order = 203 },
                new { Id = (int)value.id, Name = (string)value.name, Order = (int)value.order }
            );
        }

        [TestMethod]
        public void Deserialize_Dynamic_Object()
        {
            dynamic value = JsonConvert.DeserializeObject(@"{""id"":132, ""name"":""ditto"", ""factor"":434.1, ""object"": { ""id"":1, ""name"":""something"" } }");

            Assert.AreEqual(
                new { Id = 132, Name = "ditto", Factor = 434.1M, Object = new { Id = 1, Name = "something" } },
                new { Id = (int)value.id, Name = (string)value.name, Factor = (decimal)value.factor, Object = new { Id = (int)value.@object.id, Name = (string)value.@object.name } }
            );
        }

        [TestMethod]
        public void Deserialize_Dynamic_Array()
        {
            dynamic value = JsonConvert.DeserializeObject(@"[{""id"":132, ""name"":""ditto""}, {""id"":133, ""name"":""a""}]");

            CollectionAssert.AreEqual(
                new[] { new { Id = 132, Name = "ditto" }, new { Id = 133, Name = "a" } },
                new[] { new { Id = (int)value[0].id, Name = (string)value[0].name }, new { Id = (int)value[1].id, Name = (string)value[1].name } }
            );
        }

        [TestMethod]
        public void Deserialize_Dynamic_ObjectAndArray()
        {
            dynamic value = JsonConvert.DeserializeObject(@"{ ""item"": [{""id"":132, ""name"":""ditto""}, {""id"":133, ""name"":""a""}] }");

            Assert.Inconclusive();
        }

    }
}
