using FluentAssertions;
using lights.api.JsonConverters;
using lights.api.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace lights.api.tests
{
    public class JsonConverterTests
    {
        private class Model
        {
            public string SomeString { get; set; }
            public LightId TestedId { get; set; }
        }

        [TestCase("4")]
        [TestCase("\"FooBar\"")]
        public void ConvertId_HappyPath(string id)
        {
            var json = $"{{\"SomeString\": \"Test\", \"TestedId\": {id}}}";
            var modelInstance = JsonConvert.DeserializeObject<Model>(json, new IdConverter());

            modelInstance?.Should().NotBe(null);
            modelInstance?.SomeString.Should().Be("Test");
            modelInstance?.TestedId.Value.Should().Be(id.Replace("\"", ""));
            modelInstance?.TestedId.Should().BeEquivalentTo(new LightId(id.Replace("\"", "")));
        }
    }
}