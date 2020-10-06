using FluentAssertions;
using lights.api.JsonConverters;
using lights.api.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace lights.api.tests
{
    internal class OperatorConverterTests
    {
        private class Model
        {
            public string SomeString { get; set; }
            public Operator Op{ get; set; }
        }

        [TestCase("eq", Operator.Equal)]
        [TestCase("gt", Operator.GreaterThan)]
        public void ConvertOperator_FromJson_HappyPath(string op, Operator opEnum)
        {
            var json = $"{{\"SomeString\":\"Test\",\"Op\":\"{op}\"}}";
            var modelInstance = JsonConvert.DeserializeObject<Model>(json, new IdConverter());

            modelInstance?.Should().NotBe(null);
            modelInstance?.SomeString.Should().Be("Test");
            modelInstance?.Op.Should().Be(opEnum);
        }

        [TestCase("eq", Operator.Equal)]
        [TestCase("gt", Operator.GreaterThan)]
        public void ConvertOperator_ToJson_HappyPath(string op, Operator opEnum)
        {

            var modelInstance = new Model
            {
                SomeString = "Test",
                Op = opEnum
            };

            var expectedJson = $"{{\"SomeString\":\"Test\",\"Op\":\"{op}\"}}";

            JsonConvert.SerializeObject(modelInstance).Should().Be(expectedJson);
        }
    }
}
