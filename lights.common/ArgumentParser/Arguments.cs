using System;
using System.Collections.Generic;

namespace lights.common.ArgumentParser
{
    public class Arguments
    {
        private readonly Dictionary<string, object> _arguments;
        private readonly Queue<string> _positionalArguments;

        public int PositionalCount =>
            _positionalArguments.Count;

        public int NamedCount
            => _arguments.Count;

        public static Arguments Empty()
        {
            return new Arguments(new Dictionary<string, object>(), new List<string>());
        }

        public Arguments(Dictionary<string, object> arguments, List<string> positionalArguments)
        {
            _arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
            _positionalArguments = new Queue<string>(positionalArguments);
        }

        public T PeekValue<T>(string argumentName)
        {
            _arguments.TryGetValue(argumentName, out var value);
            return (T)value;
        }

        public T PopValue<T>(string argumentName)
        {
            _arguments.TryGetValue(argumentName, out var value);
            _arguments.Remove(argumentName);
            return (T)value;
        }

        public bool PeekFlag(string flagName)
        {
            _arguments.TryGetValue(flagName, out var value);
            if (value == null)
                return false;
            return true;
        }

        public bool PopFlag(string flagName)
        {
            _arguments.TryGetValue(flagName, out var value);
            _arguments[flagName] = false;
            if (value == null)
                return false;
            return true;
        }

        public string[] GetPositionalArguments()
        {
            return _positionalArguments
                .ToArray();
        }

        public string PeekPositionalArguments()
        {
            return _positionalArguments.Peek();
        }

        public string PopPositionalArguments()
        {
            return _positionalArguments.Dequeue();
        }

        public string[] PopPositionalArguments(int count)
        {
            var result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                result.Add(_positionalArguments.Dequeue());
            }
            return result.ToArray();
        }
    }
}
