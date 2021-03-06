﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lights.Attributes;
using lights.Extensions;

namespace lights.Controllers
{
    abstract class AbstractController
    {
        private const string DefaultControllerAction = "default";
        protected abstract string Usage { get; }

        protected string[] Arguments { get; private set; } = Array.Empty<string>();
        
        public async Task ProcessAsync(string[] arguments)
        {
            var commandAndArgs = GetCommand(arguments);

            var method = GetType()
                .GetMethods()
                .WithAttribute<CommandAttribute>(attribute => 
                    attribute.Command == commandAndArgs.command)
                .FirstOrDefault();

            if (method == null)
            {
                Default();
                return;
            }

            var attribute = method.GetAttribute<CommandAttribute>();

            var argsCount = attribute.Arguments.Length;
            if (argsCount == 0)
            {
                Arguments = commandAndArgs.args;
                var result = (Task)method.Invoke(this, null);
                await result;
                return;
            }
            
            if (argsCount <= commandAndArgs.args.Length)
            {
                Arguments = commandAndArgs.args.Skip(argsCount).ToArray();
                var result = (Task)method.Invoke(this, commandAndArgs.args.Take(argsCount).ToArray());
                await result;
                return;
            }

            Console.WriteLine("Missing argument(s) for the command.");
            Console.WriteLine(BuildCommandUsage(attribute));
        }

        [Command(DefaultControllerAction, "Default action taken")]
        public void Default()
        {
            PrintUsage();
        }

        private (string command, string[] args) GetCommand(string[] args)
        {
            if(args.Length > 0)
                return (args[0], args.Skip(1).ToArray());

            return (null, Array.Empty<string>());
        }

        protected void PrintUsage()
        {
            var attributes = GetType()
                .GetMethods()
                .WithAttribute<CommandAttribute>(attribute 
                    => attribute.Command != DefaultControllerAction)
                .Select(method =>
                    method.GetAttribute<CommandAttribute>()
                );

            var sb = new StringBuilder();
            sb.AppendLine(Usage);
            sb.AppendLine("Use one of the following sub-commands:");
            foreach (var commandAttribute in attributes)
            {
                sb.Append("  ");
                BuildCommandUsage(sb, commandAttribute);
            }
            Console.WriteLine(sb.ToString());
        }

        private string BuildCommandUsage(CommandAttribute commandAttribute)
        {
            var sb = new StringBuilder();
            return BuildCommandUsage(sb, commandAttribute)
                .ToString();
        }

        private StringBuilder BuildCommandUsage(StringBuilder sb, CommandAttribute commandAttribute)
        {
            sb.Append(commandAttribute.Command);

            foreach (var arg in commandAttribute.Arguments)
            {
                sb.Append($" <{arg}>");
            }

            sb.Append(" -- ");
            sb.Append(commandAttribute.Description);
            sb.AppendLine();

            return sb;
        }
    }
}
