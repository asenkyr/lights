using System;

namespace lights.common.ArgumentParser
{
    public class ArgumentDescriptor
    {
        public string Name { get; }
        public object DefaultValue { get; }
        public Type Type { get; }

        public ArgumentDescriptor(string shortName, Type argumentType, object defaultValue)
        {
            Name = shortName;
            DefaultValue = defaultValue;
            Type = argumentType;
        }
    }
}
