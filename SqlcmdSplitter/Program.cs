using System;

namespace SqlcmdSplitter
{
    public class Program
    {
        private const int ERROR_INVALID_COMMAND_LINE = 0x667;

        public static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args == null || args.Length < 2)
            {
                Console.WriteLine("Missing input file name");
                Console.WriteLine();
                Console.WriteLine("Syntax: dotnet SqlcmdSplitter.exe [path]");
                Environment.ExitCode = ERROR_INVALID_COMMAND_LINE;
            }
            else
            {
                Processor.Process(args[1]);
            }
        }
    }
}
