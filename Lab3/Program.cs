using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

/*
Умова:
    Одним із важливих понять, що використовуються в теорії алгоритмів, є рекурсія. Неформально її можна визначити як 
використання в описі об'єкта самого себе. Якщо йдеться про процедуру, то в процесі виконання ця процедура напряму 
або опосередковано (через інші процедури) викликає сама себе.
    Рекурсія є дуже «потужним» методом побудови алгоритмів, але таїть у собі деякі небезпеки. Наприклад, неакуратно 
написана рекурсивна процедура може увійти в нескінченну рекурсію, тобто, ніколи не завершити своє виконання (насправді, 
виконання закінчиться з переповненням стека).
    Оскільки рекурсія може бути непрямою (процедура викликає сама себе через інші процедури), то завдання визначення 
того факту, чи є дана процедура рекурсивною, досить складне. Спробуємо розв'язати простішу задачу.
    Розглянемо програму, що складається з n процедур P1, P2, ..., ..., Pn. Нехай для кожної процедури відомі процедури, 
які вона може викликати. Процедура P називається потенційно рекурсивною, якщо існує така послідовність процедур 
Q0, Q1, ..., Qk, що Q0 = Qk = P і для i = 1...k процедура Qi-1 може викликати процедуру Qi. У цьому разі завдання 
полягатиме у визначенні для кожної із заданих процедур, чи є вона потенційно рекурсивною.
    Потрібно написати програму, яка дасть змогу розв'язати названу задачу.

Вхідні дані
    Перший рядок вхідного файлу INPUT.TXT містить ціле число n - кількість процедур у програмі (1 ≤ n ≤ 100). 
Далі йдуть n блоків, що описують процедури. Після кожного блоку йде рядок, який містить 5 символів «*».
    Опис процедури починається з рядка, що містить її ідентифікатор, який складається тільки з маленьких літер 
англійського алфавіту та цифр. Ідентифікатор непорожній, і його довжина не перевищує 100 символів. Далі йде рядок, 
що містить число k (k ≤ n ) - кількість процедур, які можуть бути викликані описуваною процедурою. Наступні k рядків 
містять ідентифікатори цих процедур - по одному ідентифікатору на рядку.
    Різні процедури мають різні ідентифікатори. При цьому жодна процедура не може викликати процедуру, яка не описана 
у вхідному файлі.

Вихідні дані
    У вихідний файл OUTPUT.TXT для кожної процедури, присутньої у вхідних даних, необхідно вивести слово YES, якщо 
вона є потенційно рекурсивною, і слово NO - інакше, у тому самому порядку, в якому вони перелічені у вхідних даних.
 */


namespace Lab3
{
    public class Program
    {
        //[STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                //string inputFilePath = args.Length > 0 ? args[0] : "INPUT.TXT";
                //string outputFilePath = Path.Combine("Lab3", "OUTPUT.TXT");

                //string[] input = File.ReadAllLines(inputFilePath);
                string[] input = ["3", "p1", "2", "p1", "p2", "*****", "p2", "1", "p1", "*****", "p3", "1", "p1", "*****"];
                var (procedures, n) = ReadInput(input);


                var result = CheckProcedures(procedures);
                //File.WriteAllText(outputFilePath, string.Join(Environment.NewLine, result));

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

        // Клас для збереження інформації про процедури
        class Procedure
        {
            public string Id { get; }
            public List<string> CalledProcedures { get; }

            public Procedure(string id, List<string> calledProcedures)
            {
                Id = id;
                CalledProcedures = calledProcedures;
            }
        }

        // Метод для читання даних з файлу та перевірки
        static (List<Procedure> procedures, int n) ReadInput(string[] lines)
        {
            List<Procedure> procedures = new List<Procedure>();
            int lineIndex = 0;

            // Перевірка першого рядка: кількість процедур n
            if (!int.TryParse(lines[lineIndex], out int n) || n < 1 || n > 100)
            {
                throw new InvalidOperationException("The first line must contain an integer n (1 <= n <= 100)!");
            }
            lineIndex++;

            // Зчитування блоків процедур
            for (int i = 0; i < n; i++)
            {
                // Перевірка ідентифікатора процедури
                string procedureId = lines[lineIndex].Trim();
                if (string.IsNullOrWhiteSpace(procedureId) || procedureId.Length > 100 || !IsValidIdentifier(procedureId))
                {
                    throw new InvalidOperationException($"Invalid procedure identifier at line {lineIndex + 1}. It must be non-empty, less than 100 characters, and contain only lowercase letters or digits.");
                }
                lineIndex++;

                // Перевірка кількості викликаних процедур
                if (!int.TryParse(lines[lineIndex], out int k) || k < 0 || k > n)
                {
                    throw new InvalidOperationException($"Invalid number of called procedures for procedure {procedureId} at line {lineIndex + 1}. It must be an integer between 0 and n.");
                }
                lineIndex++;

                // Зчитування викликаних процедур
                List<string> calledProcedures = new List<string>();
                for (int j = 0; j < k; j++)
                {
                    string calledProcedureId = lines[lineIndex].Trim();
                    if (string.IsNullOrWhiteSpace(calledProcedureId) || !IsValidIdentifier(calledProcedureId))
                    {
                        throw new InvalidOperationException($"Invalid called procedure identifier at line {lineIndex + 1}. It must be non-empty and contain only lowercase letters or digits.");
                    }
                    calledProcedures.Add(calledProcedureId);
                    lineIndex++;
                }

                // Додавання процедури до списку
                procedures.Add(new Procedure(procedureId, calledProcedures));

                // Перевірка наявності рядка з «*****»
                if (lines[lineIndex] != "*****")
                {
                    throw new InvalidOperationException($"Missing termination line (*****) for procedure {procedureId} at line {lineIndex + 1}.");
                }
                lineIndex++;
            }

            return (procedures, n);
        }

        // Допоміжний метод для перевірки ідентифікаторів процедур
        static bool IsValidIdentifier(string identifier)
        {
            foreach (char c in identifier)
            {
                if (!char.IsLower(c) && !char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        // Основний метод для перевірки процедур на рекурсію
        static StringBuilder CheckProcedures(List<Procedure> procedures)
        {
            Dictionary<string, Procedure> procedureDict = procedures.ToDictionary(p => p.Id, p => p);
            Dictionary<string, bool> isRecursive = procedures.ToDictionary(p => p.Id, p => false); // тримаємо результат для кожної процедури
            StringBuilder result = new StringBuilder();

            // Перевіряємо кожну процедуру
            foreach (var procedure in procedures)
            {
                HashSet<string> visited = new HashSet<string>(); // відвідані процедури для кожної процедури
                if (!isRecursive[procedure.Id]) // якщо процедура ще не визначена як рекурсивна
                {
                    DFS(procedure.Id, procedure.Id, visited, procedureDict, isRecursive);
                }

                // Додаємо результат для цієї процедури
                result.AppendLine(isRecursive[procedure.Id] ? "YES" : "NO");
            }

            return result;
        }

        // DFS-пошук для перевірки циклів
        static bool DFS(string startProcedure, string currentProcedure, HashSet<string> visited, Dictionary<string, Procedure> procedureDict, Dictionary<string, bool> isRecursive)
        {
            // Якщо процедура вже відвідана
            if (visited.Contains(currentProcedure))
            {
                // Якщо повернулися до початкової процедури, це потенційно рекурсивна процедура
                if (currentProcedure == startProcedure)
                {
                    isRecursive[startProcedure] = true;
                    return true;
                }
                return false;
            }

            // Додаємо процедуру до відвіданих
            visited.Add(currentProcedure);

            // Перевіряємо всі виклики з цієї процедури
            foreach (var calledProcedureId in procedureDict[currentProcedure].CalledProcedures)
            {
                if (DFS(startProcedure, calledProcedureId, visited, procedureDict, isRecursive))
                {
                    return true; // Якщо знайдено цикл, можна завершити
                }
            }

            // Видаляємо процедуру після обходу (для правильного трекінгу відвіданих)
            visited.Remove(currentProcedure);
            return false;
        }
    }
}