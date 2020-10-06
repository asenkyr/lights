using System.Collections.Generic;
using FluentAssertions;
using lights.common.Collections;
using NUnit.Framework;

namespace lights.common.tests
{
    internal class MapTests
    {
        [Test]
        public void Add_Forward_Backward_HappyPath()
        {
            var map = new Map<int, string>();
            map.Add(1, "a");
            map.Add(2, "b");

            map.Forward(1).Should().Be("a");
            map.Forward(2).Should().Be("b");

            map.Backward("a").Should().Be(1);
            map.Backward("b").Should().Be(2);
        }

        [Test]
        public void FromDictionary_HappyPath()
        {
            var dict = new Dictionary<int, string>
            {
                {1, "a"},
                {2, "b"}
            };

            var map = new Map<int, string>(dict);

            map.Forward(1).Should().Be("a");
            map.Forward(2).Should().Be("b");

            map.Backward("a").Should().Be(1);
            map.Backward("b").Should().Be(2);
        }

        [Test]
        public void FromInitializer_HappyPath()
        {
            var map = new Map<int, string>
            {
                {1, "a"},
                {2, "b"}
            };

            map.Forward(1).Should().Be("a");
            map.Forward(2).Should().Be("b");

            map.Backward("a").Should().Be(1);
            map.Backward("b").Should().Be(2);
        }
    }
}
