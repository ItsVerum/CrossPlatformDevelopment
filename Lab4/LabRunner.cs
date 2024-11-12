using System;
using System.IO;
using System.Numerics;
using System.Text;
using Lab1;
using Lab2;
using Lab3;

namespace Lab4
{
    public class LabRunner
    {
        public void RunLab1(string inputFilePath, string outputFilePath)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;

                string[] input = File.ReadAllLines(inputFilePath);
                var (N, A, B) = Lab1.Program.ValidateInput(input);

                BigInteger result = Lab1.Program.CalculateNumberOfWays(N, A, B);
                File.WriteAllText(outputFilePath, result.ToString());

                Console.WriteLine("File OUTPUT.TXT successfully created");
                Console.WriteLine("LAB #1");
                Console.WriteLine("Input data:");
                Console.WriteLine(string.Join(" ", input).Trim());
                Console.WriteLine("Output data:");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine('\n');
        }

        public void RunLab2(string inputFilePath, string outputFilePath)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;

                string[] input = File.ReadAllLines(inputFilePath);
                int N = Lab2.Program.ValidateInput(input);

                long result = Lab2.Program.CalculateNumberOfPrimeNumbers(N);
                File.WriteAllText(outputFilePath, result.ToString());

                Console.WriteLine("File OUTPUT.TXT successfully created");
                Console.WriteLine("LAB #2");
                Console.WriteLine("Input data:");
                Console.WriteLine(N);
                Console.WriteLine("Output data:");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine('\n');
        }

        public void RunLab3(string inputFilePath, string outputFilePath)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;

                string[] input = File.ReadAllLines(inputFilePath);
                var (procedures, n) = Lab3.Program.ReadInput(input);


                var result = Lab3.Program.CheckProcedures(procedures);
                File.WriteAllText(outputFilePath, string.Join(Environment.NewLine, result));

                Console.WriteLine("File OUTPUT.TXT successfully created");
                Console.WriteLine("LAB #3");
                Console.WriteLine("Input data:");
                Console.WriteLine(string.Join(Environment.NewLine, input).Trim());
                Console.WriteLine("Output data:");
                Console.WriteLine(string.Join(Environment.NewLine, result));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine('\n');
        }
    }
}