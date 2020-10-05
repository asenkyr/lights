using lights.common.ArgumentParser;
using NUnit.Framework;
using FluentAssertions;

namespace lights.common.tests
{
    public class Tests
    {
        [Test]
        public void ParseArguments_Positional_Flags()
        {
            var argumentParser = ArgumentParserBuilder
                .Create()
                .AddFlag("enabled", "false")
                .Build();

            argumentParser.Parse(new[] { "foo", "bar", "--enabled", "baz" });

            argumentParser.GetFlag("enabled").Should().Be(true);

            var positionalArguments = argumentParser.GetPositionalArguments();
            positionalArguments.Length.Should().Be(3);
            positionalArguments.Should().BeEquivalentTo(new[] { "foo", "bar", "baz" });
        }

        [Test]
        public void ParseArguments_Positional_UnsetFlags()
        {
            var argumentParser = ArgumentParserBuilder
                .Create()
                .AddFlag("enabled", "false")
                .Build();

            argumentParser.Parse(new[] { "foo", "bar", "baz" });

            argumentParser.GetValue<bool>("enabled").Should().Be(false);

            var positionalArguments = argumentParser.GetPositionalArguments();
            positionalArguments.Length.Should().Be(3);
            positionalArguments.Should().BeEquivalentTo(new[] { "foo", "bar", "baz" });
        }
    }
}