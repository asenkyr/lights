using System;

namespace lights.common.ArgumentParser
{
    public class ArgumentDescriptor
    {
        public string Name { get; }
        public string DefaultValue { get; }
        public Type Type { get; }

        public ArgumentDescriptor(string shortName, Type argumentType, string defaultValue)
        {
            Name = shortName;
            DefaultValue = defaultValue;
            Type = argumentType;
        }
    }
}
