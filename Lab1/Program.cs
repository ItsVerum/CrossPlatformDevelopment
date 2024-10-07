using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

/*
Умова:
У вас є N збудованих у ряд коробок, A червоних та B синіх кульок. Всі червоні кулі (аналогічно та сині) ідентичні. 
Ви можете класти кулі у коробки. Дозволяється розміщувати в коробках кулі як одного, так і двох видів одночасно. 
Також дозволяється залишати деякі з коробок порожніми. Не обов'язково класти всі кулі у коробки.

Потрібно написати програму, яка визначає кількість різних способів, якими можна заповнити коробки кулями.

Вхідні дані
Вхідний файл INPUT.TXT містить цілі числа N, A, B. (1 ≤ N ≤ 20, 0 ≤ A, B ≤ 20)

Вихідні дані
У вихідний файл OUTPUT.TXT виведіть відповідь на завдання.
 */


namespace Lab1
{
    public class Program
    {
        //[STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string inputFilePath = args.Length > 0 ? args[0] : Path.Combine("Lab1", "INPUT.TXT");
                string outputFilePath = Path.Combine("Lab1", "OUTPUT.TXT");

                string[] input = File.ReadAllLines(inputFilePath);
                var (N, A, B) = ValidateInput(input);

                BigInteger result = CalculateNumberOfWays(N, A, B);
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

        public static (int, int, int) ValidateInput(string[] input)
        {
            //перевірка чи 3 числа
            if (input.Length != 1 || input[0].Split(' ').Length != 3)
            {
                throw new InvalidOperationException("The input file must contain 3 numbers written in one line!");
            }

            int N = Int32.MinValue;
            int A = Int32.MinValue;
            int B = Int32.MinValue;

            //перевірка чи цілі числа
            try
            {
                string[] values = input[0].Split(' ');
                N = int.Parse(values[0]);
                A = int.Parse(values[1]);
                B = int.Parse(values[2]);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Input data must be integers!");
            }

            // Перевірка умов задачі
            if (N < 1 || N > 20 || A < 0 || B < 0 || B > 20)
            {
                throw new InvalidOperationException("The numbers must meet the conditions: 1 ≤ N ≤ 20, 0 ≤ A, B ≤ 20. The numbers cannot be negative.");
            }

            return (N, A, B);
        }

        public static BigInteger CalculateNumberOfWays(int N, int A, int B)
        {
            int MAX = 41;
            long[][] c = new long[MAX][];
            for (int i = 0; i < MAX; ++i)
            {
                c[i] = new long[i + 1];
                for (int j = 0; j <= i; ++j)
                {
                    if (j == 0 || j == i)
                    {
                        c[i][j] = 1;
                    }
                    else
                    {
                        c[i][j] = c[i - 1][j] + c[i - 1][j - 1];
                    }
                }
            }

            BigInteger res = new BigInteger(c[N + A][A]);
            res *= new BigInteger(c[N + B][B]);

            return res;

        }
    }
}
