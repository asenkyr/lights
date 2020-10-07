using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lights.Attributes;
using lights.common.ArgumentParser;
using lights.Extensions;

namespace lights.Controllers
{
    abstract class AbstractController
    {
        private const string DefaultControllerAction = "default";
        protected abstract string Usage { get; }

        protected Arguments Arguments { get; private set; } = Arguments.Empty();
        
        public async Task ProcessAsync(Arguments arguments)
        {
            arguments.TryTakePositional(out var command);

            var method = GetType()
                .GetMethods()
                .WithAttribute<CommandAttribute>(attribute => 
                    attribute.Command == command)
                .FirstOrDefault();

            if (method == null)
            {
                Default();
                return;
            }

            var attribute = method.GetAttribute<CommandAttribute>();

            var requiredNumberOfArguments = attribute.Arguments.Length;
            
            if (requiredNumberOfArguments == 0)
            {
                Arguments = arguments;
                var result = (Task)method.Invoke(this, null);
                await result;
                return;
            }

            if (requiredNumberOfArguments <= arguments.PositionalArguments.Length)
            {
                var commandArguments = arguments.TakePositional(requiredNumberOfArguments);
                Arguments = arguments;
                var result = (Task)method.Invoke(this, commandArguments);
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
