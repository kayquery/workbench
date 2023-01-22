using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Threading.Tasks;
using workbench.Commands.GenerateCommand.AuthCommand;
using workbench.Commands.GenerateCommand.FakeCommand;

namespace workbench.Commands.GenerateCommand
{
    public static class GenerateCommandBuilder
    {
        private static Command _command = CommandContruct();
        public static Command GetCommand() => _command;


        public static Command CommandContruct() 
        {
            Command command = new("generate", "Generate useful data.");
            command.AddAlias("g");

            command.Add(FakeCommandBuilder.GetCommand());
            command.Add(AuthCommandBuilder.GetCommand());

            return command;
        }        
    }
}