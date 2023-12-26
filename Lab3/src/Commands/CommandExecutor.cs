using System.Text.RegularExpressions;

namespace Lab3.Commands
{
	public class CommandExecutor
	{
		private readonly Dictionary<string, ICommand> _commands = new();

		public void AddCommands(params ICommand[] commands)
		{
			foreach (var command in commands)
			{
				_commands[command.Name] = command;
			}
		}

		public void Help()
		{
			_commands["help"].Execute();
		}

		public void TryExecuteCommand(string input)
		{
			string[] parts = Regex.Split(input.Trim(), @"\s+");
			string? name = parts.ElementAtOrDefault(0);

			if (name == null)
			{
				return;
			}

			if (_commands.TryGetValue(name, out var command))
			{
				var args = parts.Skip(1).ToArray();
				command.Execute(args);
			}
			else
			{
				throw new ExecutionException("Unknown command. Use 'help' to see all commands.");
			}
		}

		public bool CommandIsValid(string command)
		{
			var parts = Regex.Split(command.Trim(), @"\s+");
			var name = parts.ElementAtOrDefault(0);

			return name != null && _commands.ContainsKey(name);
		}

		public static int? ParseIntArg(string arg)
		{
			if (int.TryParse(arg, out int result))
			{
				return result;
			}

			return null;
		}
	}
}
