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
            Console.WriteLine("Please specify a command:");
        }

        private void OnUnknownCommand(CommandLineApplication app)
        {
            Console.WriteLine("Unrecognized command. Available commands:");
            Console.WriteLine(" - version: Display application version and author information");
            Console.WriteLine(" - run: Run a specific lab");
            Console.WriteLine(" - set-path: Define custom input/output file path");
        }
    }

    [Command(Name = "version", Description = "Display application version and author information")]
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
        [Argument(0, "lab", "Specify the lab to execute (e.g., lab1)")]
        public string? LabName { get; set; }

        [Option("-i|--input", "Input file", CommandOptionType.SingleValue)]
        public string? InputFile { get; set; }

        [Option("-o|--output", "Output file", CommandOptionType.SingleValue)]
        public string? OutputFile { get; set; }

        private void OnExecute()
        {
            string? labPath = GetLabDirectory(LabName);
            if (labPath == null)
            {
                Console.WriteLine($"Lab '{LabName}' not recognized. Available labs: lab1, lab2, lab3.");
                return;
            }

            Console.WriteLine(Environment.GetEnvironmentVariable("LAB_PATH"));
            string inputFilePath = InputFile ?? Environment.GetEnvironmentVariable("LAB_PATH") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "INPUT.TXT");
            string outputFilePath = OutputFile ?? Path.Combine(labPath, "OUTPUT.TXT");

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Input file '{inputFilePath}' does not exist.");
                return;
            }

            var labProcessor = new LabRunner();
            switch (LabName.ToLower())
            {
                case "lab1":
                    labProcessor.RunLab1(inputFilePath, outputFilePath);
                    break;
                case "lab2":
                    labProcessor.RunLab2(inputFilePath, outputFilePath);
                    break;
                case "lab3":
                    labProcessor.RunLab3(inputFilePath, outputFilePath);
                    break;
                default:
                    Console.WriteLine("Unrecognized lab specified.");
                    break;
            }
            Console.WriteLine($"Lab {LabName} processed. Output saved to {outputFilePath}");
        }

        private string? GetLabDirectory(string labName)
        {
            string rootDirectory = Directory.GetCurrentDirectory();
            switch (labName.ToLower())
            {
                case "lab1":
                    return Path.Combine(rootDirectory, "LAB4", "LAB1");
                case "lab2":
                    return Path.Combine(rootDirectory, "LAB4", "LAB2");
                case "lab3":
                    return Path.Combine(rootDirectory, "LAB4", "LAB3");
                default:
                    return null;
            }
        }
    }

    [Command(Name = "set-path", Description = "Define custom input/output file path")]
    class SetPathCommand
    {
        [Option("-p|--path", "Custom path for input/output files", CommandOptionType.SingleValue)]
        public required string Path { get; set; }

        private void OnExecute()
        {
            Environment.SetEnvironmentVariable("LAB_PATH", Path);
            Console.WriteLine($"Custom path set to: {Path}");
        }
    }
}