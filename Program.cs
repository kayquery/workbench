using System.CommandLine;
using workbench.Commands.FakeCommand;

namespace Workbench
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RootCommand rootCommand = new("A tool to do the boring work");
            ConfigureRootCommand(rootCommand);
        
            rootCommand.Invoke(args);
        }


        private static void ConfigureRootCommand(RootCommand rootCommand)
        {
            AddCommands(rootCommand);
            SetRootHandler(rootCommand);
        }

        private static void AddCommands(RootCommand rootCommand)
        {
            rootCommand.Add(FakeCommandBuilder.GetCommand());
        }

        private static void SetRootHandler(RootCommand rootCommand)
        {
            rootCommand.SetHandler( () =>
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Welcome to Workbench Cli");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nTo get HELP to start, use '-h', '-?' or '--help'");
            });
        }
    }
}