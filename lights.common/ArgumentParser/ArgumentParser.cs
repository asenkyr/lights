using System;
using System.Collections.Generic;
using System.Linq;

namespace lights.common.ArgumentParser
{
    public class ArgumentParser
    {
        

        private readonly Dictionary<string, ArgumentDescriptor> _argumentDescriptors;

        public ArgumentParser(Dictionary<string, ArgumentDescriptor> argumentDescriptors)
        {
            _argumentDescriptors = argumentDescriptors ?? throw new ArgumentNullException(nameof(argumentDescriptors));
        }

        public Arguments Parse(string[] args)
        {
            var arguments = new Dictionary<string, object>();
            var positionalArguments = new List<string>();

            PreFillArguments(arguments);

            using var enumerator = args
                .AsEnumerable()
                .GetEnumerator();

            while (enumerator.MoveNext())
            {
                string current = enumerator.Current;
                if (!IsArgument(current))
                {
                    positionalArguments.Add(current);
                    continue;
                }

                if(!_argumentDescriptors.TryGetValue(NormalizeArgName(current), out var descriptor))
                    throw new ArgumentException($"Unknown argument '{current}'");
                
                if (descriptor.Type == typeof(bool))
                {
                    arguments[NormalizeArgName(current)] = true;
                    continue;
                }

                enumerator.MoveNext();
                EnsureValue(current);
                var currentValue = ConvertValue(enumerator.Current, descriptor.Type);

                arguments[NormalizeArgName(current)] = currentValue;
            }

            return new Arguments(arguments, positionalArguments);
        }

        private void PreFillArguments(Dictionary<string, object> arguments)
        {
            foreach (var argDesriptor in _argumentDescriptors)
            {
                arguments[argDesriptor.Key] = argDesriptor.Value.DefaultValue;
            }
        }

        private bool IsArgument(string value)
        {
            return value.StartsWith("-") || value.StartsWith("--");
        }

        private void EnsureValue(string value)
        {
            if(!IsArgument(value))
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

            if (type == typeof(int?))
            {
                if (value == null)
                    return null;
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
