using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace workbench.Util
{
    public static class ClipboardManager
    {
        public static void CopyToClipboard(string data)
        {
            string filename,arguments;

            if(!IsOSSuported())
                return;

            (filename, arguments) = ("cmd.exe", $"/c echo {data} | clip ");

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

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nIt's already copied to your clipboard!");
        }

        private static bool IsOSSuported()
        {
            if(!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("Copy to clipboard not supported");
                return false;
            }
            return true;
        }
    }
}