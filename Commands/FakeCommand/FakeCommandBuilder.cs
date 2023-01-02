using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Bogus;

namespace workbench.Commands.FakeCommand
{
    public static class FakeCommandBuilder 
    {
        private static Argument<EDataType> DataTypeArgument = new("data-type", "the data type to generate");

        private static Command _command = CommandConstruct();
        public static Command GetCommand() => _command;

        


        private static Command CommandConstruct()
        {
            Command command = new("fake", "generate fake data");
            
            command.AddArgument(DataTypeArgument);

            command.SetHandler((dataType) => 
            {
                Faker faker = new("pt_BR");
                string fakeData = "";

                fakeData = dataType switch
                {
                    EDataType.Email => faker.Person.Email,
                    EDataType.Name => faker.Person.FullName,
                    _ =>  ""
                };

                Console.WriteLine("The follow fake data was generate:\n\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(fakeData);
                Console.ForegroundColor = ConsoleColor.White;
                CopyToClipBoard(fakeData);
            }, DataTypeArgument);

            return command;
        }

        private static void CopyToClipBoard(string data)
        {
            string filename,arguments;
            // data = data.Replace("\"", "\\\"");

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                (filename, arguments) = ("cmd.exe", $"/c echo \"{data}\" | clip ");
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                { Console.WriteLine("Copyboard does not support Linux"); return; }
            else
                { Console.WriteLine("Copyboard does not support Mac"); return; }

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = filename,
                    Arguments = arguments, 
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                }
            };
            process.Start();
            process.WaitForExit();

            Console.WriteLine("\n\nIt's already copied to your clipboard!");           
        }
    }
}