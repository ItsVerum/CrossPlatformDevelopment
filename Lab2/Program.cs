using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;


namespace Lab2
{
    public class Program
    {
        //[STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string inputFilePath = args.Length > 0 ? args[0] : "INPUT.TXT";
                string outputFilePath = Path.Combine("Lab2", "OUTPUT.TXT");

                string[] input = File.ReadAllLines(inputFilePath);
                int N = ValidateInput(input);

                long result = CalculateNumberOfPrimeNumbers(N);
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

        public static int ValidateInput(string[] input)
        {
            //перевірка чи 1 число
            if (input.Length != 1 || input[0].Trim().Split(' ').Length != 1)
            {
                throw new InvalidOperationException("The input file must contain 1 number written in first line!");
            }

            int N = Int32.MinValue;

            //перевірка чи ціле число
            try
            {
                N = int.Parse(input[0].Trim());
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Input data must be integers!");
            }

            // Перевірка умов задачі
            if (N < 3 || N > 10000)
            {
                throw new InvalidOperationException("The number must meets the conditions: 3 ≤ N ≤ 10 000.");
            }

            return N;
        }

        public static long CalculateNumberOfPrimeNumbers(int N)
        {
            const int MOD = 1000000009;

            // Якщо N == 3, просто повертаємо кількість тризначних простих чисел
            if (N == 3) return ThreeDigitPrimeNumbers().Count();

            var prefixes = PrefixCorrespondance(ThreeDigitPrimeNumbers());
            var previous = new long[90]; // Зберігаємо лише попередній ряд
            var current = new long[90];  // І поточний ряд

            // Ініціалізація для першого рядка
            for (var counter = 10; counter < 100; counter++)
                previous[counter - 10] = prefixes.TryGetValue(counter, out var collection) ? collection.Count() : 0;

            // Обчислюємо значення для кожного ряду, не зберігаючи зайві ряди
            for (var counter = 1; counter <= N - 3; counter++)
            {
                Array.Clear(current, 0, current.Length); // Очищуємо поточний ряд

                foreach (var pair in prefixes)
                {
                    foreach (var primeNumber in pair.Value)
                    {
                        var suffix = primeNumber % 100 - 10;
                        if (suffix >= 0)
                        {
                            current[pair.Key - 10] = (current[pair.Key - 10] + previous[suffix]) % MOD;
                        }
                    }
                }

                // Копіюємо поточний ряд у попередній для наступної ітерації
                Array.Copy(current, previous, 90);
            }

            // Підсумовуємо всі значення з останнього ряду
            long sum = 0;
            for (var counter = 0; counter < 90; counter++)
                sum = (sum + previous[counter]) % MOD;

            return sum;
        }

        // Функція для отримання всіх тризначних простих чисел
        public static IEnumerable<int> ThreeDigitPrimeNumbers()
        {
            return Enumerable.Range(100, 900).Where(number => IsPrime(number));
        }

        // Відповідність префіксів
        public static Dictionary<int, IEnumerable<int>> PrefixCorrespondance(IEnumerable<int> primeNumbers)
        {
            return primeNumbers
                .GroupBy(number => number / 10)
                .ToDictionary(group => group.Key, group => group.Select(item => item));
        }

        // Перевірка, чи є число простим
        public static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}

