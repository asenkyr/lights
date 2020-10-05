using System;

namespace lights.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    class CommandAttribute : Attribute
    {
        public string Command { get; }
        public string Description { get; }
        public string[] Arguments { get; }

        public CommandAttribute(string command, string description)
            : this(command, description, Array.Empty<string>())
        {
        }

        public CommandAttribute(string command, string description, params string[] arguments)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Arguments = arguments;
        }
    }
}
