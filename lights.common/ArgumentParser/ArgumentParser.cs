using System;
using System.Collections.Generic;
using System.Linq;

namespace lights.common.ArgumentParser
{
    public class ArgumentParser
    {
        private Dictionary<string, object> _arguments = new Dictionary<string, object>();
        private List<string> _positionalArguments = new List<string>();

        private Dictionary<string, ArgumentDescriptor> _argumentDescriptors;

        public ArgumentParser(Dictionary<string, ArgumentDescriptor> argumentDescriptors)
        {
            _argumentDescriptors = argumentDescriptors ?? throw new ArgumentNullException(nameof(argumentDescriptors));
        }

        public void Parse(string[] args)
        {
            using var enumerator = args
                .AsEnumerable()
                .GetEnumerator();

            while (enumerator.MoveNext())
            {
                string current = enumerator.Current;
                if (!IsArgument(current))
                {
                    _positionalArguments.Add(current);
                    continue;
                }

                var descriptor = _argumentDescriptors[NormalizeArgName(current)];
                
                if (descriptor.Type == typeof(bool))
                {
                    _arguments[NormalizeArgName(current)] = true;
                    continue;
                }

                enumerator.MoveNext();
                EnsureValue(current);
                var currentValue = ConvertValue(enumerator.Current, descriptor.Type);

                _arguments[NormalizeArgName(current)] = currentValue;
            }
        }

        public T GetValue<T>(string argumentName)
        {
            _arguments.TryGetValue(argumentName, out var value);

            if (value == null)
            {
                var descriptor = _argumentDescriptors[argumentName];
                return (T)ConvertValue(descriptor.DefaultValue, descriptor.Type);
            }

            return (T)value;
        }

        public bool GetFlag(string flagName)
        {
            _arguments.TryGetValue(flagName, out var value);
            if (value == null)
                return false;
            return true;
        }

        public string[] GetPositionalArguments()
        {
            return _positionalArguments
                .ToArray();
        }

        private bool IsArgument(string value)
        {
            return value.StartsWith("-") || value.StartsWith("--");
        }

        private void EnsureValue(string value)
        {
            if(IsArgument(value))
                throw new ArgumentException(
                    "Was expecting value for previous argument, but another argument found.");
        }

        private object ConvertValue(string value, Type type)
        {
            if (type == typeof(string))
            {
                return value;
            }

            if (type == typeof(int))
            {
                return int.Parse(value);
            }

            if (type == typeof(bool))
            {
                return bool.Parse(value);
            }

            if (type == typeof(double))
            {
                return double.Parse(value);
            }

            throw new ArgumentException($"Tried to convert to unsupported type '{type}'");
        }

        private string NormalizeArgName(string argument)
        {
            return argument.Replace("-", "");
        }
    }
}
