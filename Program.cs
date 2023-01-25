using System.CommandLine;
using System.Reflection;
using workbench.Commands.GenerateCommand;

namespace Workbench
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RootCommand rootCommand = new("A tool to help with simple jobs");
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
            rootCommand.AddCommand(GenerateCommandBuilder.GetCommand());
        }

        private static void SetRootHandler(RootCommand rootCommand)
        {
            rootCommand.SetHandler( () =>
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(" __    __           _    _                     _     ");
                Console.WriteLine("/ / /\\ \\ \\___  _ __| | _| |__   ___ _ __   ___| |__  ");
                Console.WriteLine("\\ \\/  \\/ / _ \\| '__| |/ / '_ \\ / _ \\ '_ \\ / __| '_ \\ ");
                Console.WriteLine(" \\  /\\  / (_) | |  |   <| |_) |  __/ | | | (__| | | |");
                Console.WriteLine("  \\/  \\/ \\___/|_|  |_|\\_\\_.__/ \\___|_| |_|\\___|_| |_|");
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("\n\nWorkbench Tool - version 1.0.0");

                Console.WriteLine("\nType -h, -? or --help to start.");
            });
        }
    }
}