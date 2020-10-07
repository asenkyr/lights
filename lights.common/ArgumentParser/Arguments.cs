using System;
using System.Collections.Generic;

namespace lights.common.ArgumentParser
{
    public class Arguments
    {
        private readonly Dictionary<string, object> _arguments;
        private readonly Queue<string> _positionalArguments;

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

        public T TakeValue<T>(string argumentName)
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

        public bool TakeFlag(string flagName)
        {
            _arguments.TryGetValue(flagName, out var value);
            _arguments[flagName] = false;
            if (value == null)
                return false;
            return (bool)value;
        }

        public string[] PositionalArguments 
            => _positionalArguments
                .ToArray();

        public string PeekPositional()
        {
            return _positionalArguments.Peek();
        }

        public string TakePositional()
        {
            return _positionalArguments.Dequeue();
        }

        public bool TryTakePositional(out string result)
        {
            return _positionalArguments.TryDequeue(out result);
        }

        public string[] TakePositional(int count)
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
