using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Lab5.Controllers
{
    public class LabController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public LabController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult LAB1()
        {
            var model = new LabViewModel
            {
                TaskNumber = "1",
                TaskVariant = "17",
                TaskDescription = "У вас є N збудованих у ряд коробок, A червоних та B синіх кульок. Всі червоні кулі (аналогічно та сині) ідентичні. \r\n" +
                "Ви можете класти кулі у коробки. Дозволяється розміщувати в коробках кулі як одного, так і двох видів одночасно. \r\n" +
                "Також дозволяється залишати деякі з коробок порожніми. Не обов'язково класти всі кулі у коробки.\r\n\r\n" +
                "Потрібно написати програму, яка визначає кількість різних способів, якими можна заповнити коробки кулями.\r\n",
                InputDescription = "Вхідний файл INPUT.TXT містить цілі числа N, A, B. (1 ≤ N ≤ 20, 0 ≤ A, B ≤ 20)",
                OutputDescription = "У вихідний файл OUTPUT.TXT виведіть відповідь на завдання.",
                TestCases = new List<TestCase>
                {
                    new TestCase { Input = "5 1 3", Output = "336" },
                }
            };
            return View(model);
        }

        public IActionResult LAB2()
        {
            var model = new LabViewModel
            {
                TaskNumber = "2",
                TaskVariant = "17",
                TaskDescription = "Будемо називати натуральне число трипростим, якщо в ньому будь-які 3 цифри, що йдуть підряд, утворюють тризначне просте число." +
                "\r\n\r\nПотрібно знайти кількість N-значних трипростих чисел.\r\n",
                InputDescription = "Вхідний файл INPUT.TXT містить натуральне число N (3 ≤ N ≤ 10000).",
                OutputDescription = "Вихідний файл OUTPUT.TXT повинен містити кількість N-значних трипростих чисел, яку слід вивести за модулем 10^9+9.",
                TestCases = new List<TestCase>
                {
                    new TestCase { Input = "4", Output = "204" },
                }
            };
            return View(model);
        }

        public IActionResult LAB3()
        {
            var model = new LabViewModel
            {
                TaskNumber = "3",
                TaskVariant = "17",
                TaskDescription = "Одним із важливих понять, що використовуються в теорії алгоритмів, є рекурсія. Неформально її можна визначити як " +
                "використання в описі об'єкта самого себе. Якщо йдеться про процедуру, то в процесі виконання ця процедура напряму або опосередковано" +
                " (через інші процедури) викликає сама себе.\r\nРекурсія є дуже «потужним» методом побудови алгоритмів, але таїть у собі деякі небезпеки." +
                " Наприклад, неакуратно написана рекурсивна процедура може увійти в нескінченну рекурсію, тобто, ніколи не завершити своє виконання " +
                "(насправді, виконання закінчиться з переповненням стека).\r\nОскільки рекурсія може бути непрямою (процедура викликає сама себе через" +
                " інші процедури), то завдання визначення того факту, чи є дана процедура рекурсивною, досить складне. Спробуємо розв'язати простішу " +
                "задачу.\r\nРозглянемо програму, що складається з n процедур P1, P2, ..., ..., Pn. Нехай для кожної процедури відомі процедури, які вона" +
                " може викликати. Процедура P називається потенційно рекурсивною, якщо існує така послідовність процедур Q0, Q1, ..., Qk, що Q0 = Qk = P" +
                " і для i = 1...k процедура Qi-1 може викликати процедуру Qi. У цьому разі завдання полягатиме у визначенні для кожної із заданих " +
                "процедур, чи є вона потенційно рекурсивною.\r\nПотрібно написати програму, яка дасть змогу розв'язати названу задачу.",
                InputDescription = "Перший рядок вхідного файлу INPUT.TXT містить ціле число n - кількість процедур у програмі (1 ≤ n ≤ 100). " +
                "Далі йдуть n блоків, що описують процедури. Після кожного блоку йде рядок, який містить 5 символів «*»." +
                "Опис процедури починається з рядка, що містить її ідентифікатор, який складається тільки з маленьких літер англійського алфавіту та цифр." +
                " Ідентифікатор непорожній, і його довжина не перевищує 100 символів. Далі йде рядок, \r\nщо містить число k (k ≤ n ) - кількість процедур, " +
                "які можуть бути викликані описуваною процедурою. Наступні k рядків містять ідентифікатори цих процедур - по одному ідентифікатору на рядку." +
                "\r\nРізні процедури мають різні ідентифікатори. При цьому жодна процедура не може викликати процедуру, яка не описана у вхідному файлі.",
                OutputDescription = "У вихідний файл OUTPUT.TXT для кожної процедури, присутньої у вхідних даних, необхідно вивести слово YES, якщо вона" +
                " є потенційно рекурсивною, і слово NO - інакше, у тому самому порядку, в якому вони перелічені у вхідних даних.",
                TestCases = new List<TestCase>
                {
                    new TestCase
                    {
                        Input = "3\np1\n2\np1\np2\n*****\np2\n1\np1\n*****\np3\n1\np1\n*****",
                        Output = "YES\nYES\nNO"
                    }
                }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLab(int labNumber, IFormFile inputFile)
        {
            if (inputFile == null || inputFile.Length == 0)
                return BadRequest("Please upload a file");

            string[] lines;
            using (var reader = new StreamReader(inputFile.OpenReadStream()))
            {
                var fileContent = await reader.ReadToEndAsync();
                lines = fileContent.Split(Environment.NewLine);
            }

            string output;

            switch (labNumber)
            {
                case 1:
                    var (N, A, B) = Lab1.Program.ValidateInput(lines);
                    output = Lab1.Program.CalculateNumberOfWays(N, A, B).ToString();
                    break;
                case 2:
                    int n = Lab2.Program.ValidateInput(lines);
                    output = Lab2.Program.CalculateNumberOfPrimeNumbers(n).ToString();
                    break;
                case 3:
                    var (procedures, n2) = Lab3.Program.ReadInput(lines);
                    var resultLab3 = Lab3.Program.CheckProcedures(procedures);
                    output = string.Join(Environment.NewLine, resultLab3);
                    break;
                default:
                    return BadRequest("Invalid lab number");
            }

            var result = new { output = output };
            return Json(result);
        }

    }
}
