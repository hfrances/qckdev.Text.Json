using Microsoft.VisualStudio.TestTools.UnitTesting;
using qckdev.Text.Json.Test.Common;
using qckdev.Text.Json.Test.Common.TestObjects;
using System;

namespace qckdev.Text.Json.Test
{
    [TestClass]
    public class JsonConverterTest : Common.IJsonConverterTest
    {

        Common.JsonConverterTest InnerTest = new Common.JsonConverterTest(new Assert(), new CollectionAssert());

        [TestMethod]
        public void SerializeAndDeserialize_Temperature()
        {
            InnerTest.SerializeAndDeserialize_Temperature();
        }

        [TestMethod]
        public void Deserialize_Pokemon()
        {
            InnerTest.Deserialize_Pokemon();
        }

    }
}
