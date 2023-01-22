using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Bogus;
using workbench.Util;

namespace workbench.Commands.GenerateCommand.FakeCommand
{
    public static class FakeCommandBuilder 
    {
        private static Argument<EFakeDataType> DataTypeArgument = new("data-type", "the fake data type to generate.");

        private static Command _command = CommandConstruct();
        public static Command GetCommand() => _command;




        private static Command CommandConstruct()
        {
            Command command = new("fake", "Generate fake data");
            command.AddAlias("f");

            command.AddArgument(DataTypeArgument);

            command.SetHandler((dataType) => 
            {
                Faker faker = new("pt_BR");
                string fakeDataResult = "";

                fakeDataResult = dataType switch
                {
                    EFakeDataType.Email => faker.Person.Email,
                    EFakeDataType.Name => faker.Person.FullName,
                    _ =>  ""
                };

                PrintResult(fakeDataResult);
            }, DataTypeArgument);

            return command;
        }

        private static void PrintResult(string result)
        {                
            Console.WriteLine("The follow fake data was generated:\n\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.White;
            ClipboardManager.CopyToClipboard(result);
        }





    }
}