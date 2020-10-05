using System;
using System.Collections.Generic;
using System.Text;

namespace lights.common.ArgumentParser
{
    public class ArgumentParserBuilder
    {
        private Dictionary<string, ArgumentDescriptor> _argumentDescriptors = new Dictionary<string, ArgumentDescriptor>();

        public static ArgumentParserBuilder Create()
        {
            return new ArgumentParserBuilder();
        }

        public ArgumentParserBuilder AddFlag(string shortName, string defaultValue)
        {
            _argumentDescriptors[shortName] = new ArgumentDescriptor(shortName, typeof(bool), defaultValue);
            return this;
        }

        public ArgumentParserBuilder AddArgument<T>(string shortName, string name, string defaultValue)
        {
            _argumentDescriptors[shortName] = new ArgumentDescriptor(shortName, typeof(T), defaultValue);
            return this;
        }

        public ArgumentParser Build()
        {
            return new ArgumentParser(_argumentDescriptors);
        }
    }
}
