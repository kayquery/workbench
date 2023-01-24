using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Threading.Tasks;

namespace workbench.Commands.NewCommand
{
    public static class NewCommandBuilder
    {
        public static Argument<EDatabase> DatabaseArgument = new("database", "Set the kind of project based on the choosed database");

        private static Command _command = CommandConstruct();
        public static Command GetCommand() => _command;
        
        
        private static Command CommandConstruct()
        {
            Command command = new("new", "Create a new file/project");
            command.AddAlias("n");

            command.AddArgument(DatabaseArgument);

            command.SetHandler((database) => 
            {
                DownloadBaseProject(database);

                PrintResult(database);
            }, DatabaseArgument);

            return command;
        }

        private static void PrintResult(EDatabase database)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nNew project created");
        }

        private static void DownloadBaseProject(object database)
        {
 
        }
    }
}