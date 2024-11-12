using McMaster.Extensions.CommandLineUtils;
using System;
using System.Runtime.InteropServices;

namespace Lab4 
{ 
    [Command(Name = "LAB4", Description = "Console app for labs")]
    [Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
    class Program
    {
        static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            Console.WriteLine("Specify a command");
        }

        private void OnUnknownCommand(CommandLineApplication app)
        {
            Console.WriteLine("Unknown command. Use one of the following:");
            Console.WriteLine(" - version: Displays app version and author's name");
            Console.WriteLine(" - run: Run a specific lab");
            Console.WriteLine(" - set-path: Set input/output path");
        }
    }

    [Command(Name = "version", Description = "Displays app version and author")]
    class VersionCommand
    {
        private void OnExecute()
        {
            Console.WriteLine("Author: Nychyporuk Veronika IPЗ-32");
            Console.WriteLine("Version: 1.0.0");
        }
    }

    [Command(Name = "run", Description = "Run a specific lab")]
    class RunCommand
    {
        [Argument(0, "lab", "Specify lab to run (lab1)")]
        public string? Lab { get; set; }

        [Option("-I|--input", "Input file", CommandOptionType.SingleValue)]
        public string? InputFile { get; set; }

        [Option("-o|--output", "Output file", CommandOptionType.SingleValue)]
        public string? OutputFile { get; set; }


        private void OnExecute()
        {
            string? labPath = GetLabDirectory(Lab);
            if (labPath == null)
            {
                Console.WriteLine($"Unknown lab '{Lab}'. Available labs: lab1.");
                return;
            }

            // Перевірка пріоритетності шляху
            Console.WriteLine(Environment.GetEnvironmentVariable("LAB_PATH"));
            string inputFilePath = InputFile ?? Environment.GetEnvironmentVariable("LAB_PATH") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "INPUT.TXT");
            string outputFilePath = OutputFile ?? Path.Combine(labPath, "OUTPUT.TXT");

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Input file '{inputFilePath}' not found.");
                return;
            }

            var runner = new LabRunner();

            switch (Lab.ToLower())
            {
                case "lab1":
                    runner.RunLab1(inputFilePath, outputFilePath);
                    break;
                case "lab2":
                    runner.RunLab2(inputFilePath, outputFilePath);
                    break;
                case "lab3":
                    runner.RunLab3(inputFilePath, outputFilePath);
                    break;
                default:
                    Console.WriteLine("Unknown lab specified.");
                    break;
            }

            Console.WriteLine($"Lab {Lab} processed. Output saved to {outputFilePath}");
        }

        private string? GetLabDirectory(string labName)
        {
            string projectRoot = Directory.GetCurrentDirectory();


            switch (labName.ToLower())
            {
                case "lab1":
                    return Path.Combine(projectRoot, "LAB4", "LAB1");
                case "lab2":
                    return Path.Combine(projectRoot, "LAB4", "LAB2");
                case "lab3":
                    return Path.Combine(projectRoot, "LAB4", "LAB3");
                default:
                    return null;
            }
        }
    }

    [Command(Name = "set-path", Description = "Set input/output path")]
    class SetPathCommand
    {
        [Option("-p|--path", "Path to input/output files", CommandOptionType.SingleValue)]
        public required string Path { get; set; }

        private void OnExecute()
        {
            Environment.SetEnvironmentVariable("LAB_PATH", Path);
            Console.WriteLine($"Path set to: {Path}");
        }
    }
}