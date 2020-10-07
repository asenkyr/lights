using System;
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
                .AddFlag("enabled", false)
                .Build();

            var arguments = argumentParser.Parse(new[] { "foo", "bar", "--enabled", "baz" });
            arguments.PeekFlag("enabled").Should().Be(true);

            var positionalArguments = arguments.PositionalArguments;
            positionalArguments.Length.Should().Be(3);
            positionalArguments.Should().BeEquivalentTo(new[] { "foo", "bar", "baz" });
        }

        [Test]
        public void ParseArguments_PoppedFlags()
        {
            var argumentParser = ArgumentParserBuilder
                .Create()
                .AddFlag("enabled", false)
                .Build();

            var arguments = argumentParser.Parse(new[] { "foo", "bar", "--enabled", "baz" });
            arguments.TakeFlag("enabled").Should().Be(true);
            arguments.TakeFlag("enabled").Should().Be(false);
        }

        [Test]
        public void ParseArguments_PoppedArgument()
        {
            var argumentParser = ArgumentParserBuilder
                .Create()
                .AddArgument("enabled", 150)
                .Build();

            var arguments = argumentParser.Parse(new[] { "foo", "bar", "--enabled", "300" });
            arguments.TakeValue<int?>("enabled").Should().Be(300);
            arguments.TakeValue<int?>("enabled").Should().Be(null);
        }

        [Test]
        public void ParseArguments_Positional_UnsetFlags()
        {
            var argumentParser = ArgumentParserBuilder
                .Create()
                .AddFlag("enabled", false)
                .Build();

            var arguments = argumentParser.Parse(new[] { "foo", "bar", "baz" });

            arguments.PeekValue<bool>("enabled").Should().Be(false);

            var positionalArguments = arguments.PositionalArguments;
            positionalArguments.Length.Should().Be(3);
            positionalArguments.Should().BeEquivalentTo(new[] { "foo", "bar", "baz" });
        }

        [Test]
        public void ParseArguments_Positional_Arg()
        {
            var argumentParser = ArgumentParserBuilder
                .Create()
                .AddArgument<int>("enabled", 150)
                .Build();

            var arguments = argumentParser.Parse(new[] { "foo", "bar", "baz", "--enabled", "400" });

            arguments.PeekValue<int>("enabled").Should().Be(400);

            var positionalArguments = arguments.PositionalArguments;
            positionalArguments.Length.Should().Be(3);
            positionalArguments.Should().BeEquivalentTo(new[] { "foo", "bar", "baz" });
        }

        [Test]
        public void ParseArguments_Positional_UnsetArg()
        {
            var argumentParser = ArgumentParserBuilder
                .Create()
                .AddArgument<int>("enabled", 150)
                .Build();

            var arguments = argumentParser.Parse(new[] { "foo", "bar", "baz" });

            arguments.PeekValue<int>("enabled").Should().Be(150);

            var positionalArguments = arguments.PositionalArguments;
            positionalArguments.Length.Should().Be(3);
            positionalArguments.Should().BeEquivalentTo(new[] { "foo", "bar", "baz" });
        }

        [Test]
        public void ParseArguments_UnknownNamedArg()
        {
            var argumentParser = ArgumentParserBuilder
                .Create()
                .Build();

            var parse = new Action(() => argumentParser.Parse(new[] { "foo", "bar", "baz", "--enabled" }));
            parse.Should().Throw<ArgumentException>();
        }
    }
}