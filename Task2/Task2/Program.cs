using System;

namespace Task2_Payment
{
    /// <summary>
    /// Главный класс программы для расчета заработной платы
    /// </summary>
    class Program
    {
        /// <summary>
        /// Главный метод программы.
        /// </summary>
        /// <param name="args">
        /// Аргументы командной строки.
        /// </param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ПРОГРАММА ДЛЯ РАСЧЕТА ЗАРПЛАТЫ ===\n");

            string fullName = ReadFullName();

            double salary =
                ReadPositiveDouble(
                    "Введите оклад (в рублях): ");

            int yearOfEmployment =
                ReadEmploymentYear();

            double bonusPercent =
                ReadNonNegativeDouble(
                    "Введите процент надбавки: ");

            double incomeTax =
                ReadNonNegativeDouble(
                    "Введите подоходный налог (%): ");

            int workingDaysInMonth =
                ReadPositiveInt(
                    "Введите количество рабочих дней в месяце: ");

            int daysWorked =
                ReadWorkedDays(
                    workingDaysInMonth);

            Payment employee = new Payment(
                fullName,
                salary,
                yearOfEmployment,
                bonusPercent,
                incomeTax,
                daysWorked,
                workingDaysInMonth);

            employee.CalculateAccruedAmount();
            employee.CalculateWithheldAmount();

            employee.DisplayInfo();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        /// <summary>
        /// Считывает ФИО пользователя.
        /// </summary>
        /// <returns>
        /// Корректное ФИО.
        /// </returns>
        private static string ReadFullName()
        {
            while (true)
            {
                Console.Write("Введите фамилию, имя, отчество: ");

                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("Ошибка: ФИО не может быть пустым!");
            }
        }

        /// <summary>
        /// Считывает положительное число.
        /// </summary>
        /// <param name="message">
        /// Сообщение пользователю.
        /// </param>
        /// <returns>
        /// Корректное число.
        /// </returns>
        private static double ReadPositiveDouble(string message)
        {
            while (true)
            {
                Console.Write(message);

                bool success = double.TryParse(
                    Console.ReadLine(),
                    out double result);

                if (!success)
                {
                    Console.WriteLine("Ошибка: Введите число.");
                    continue;
                }

                if (result <= 0)
                {
                    Console.WriteLine("Ошибка: Значение должно быть больше нуля.");
                    continue;
                }

                return result;
            }
        }

        /// <summary>
        /// Считывает неотрицательное число.
        /// </summary>
        /// <param name="message">
        /// Сообщение пользователю.
        /// </param>
        /// <returns>
        /// Корректное число.
        /// </returns>
        private static double ReadNonNegativeDouble(string message)
        {
            while (true)
            {
                Console.Write(message);

                bool success = double.TryParse(
                    Console.ReadLine(),
                    out double result);

                if (!success)
                {
                    Console.WriteLine("Ошибка: Введите число.");
                    continue;
                }

                if (result < 0)
                {
                    Console.WriteLine("Ошибка: Значение не может быть отрицательным.");
                    continue;
                }

                return result;
            }
        }

        /// <summary>
        /// Считывает положительное целое число.
        /// </summary>
        /// <param name="message">
        /// Сообщение пользователю.
        /// </param>
        /// <returns>
        /// Корректное значение.
        /// </returns>
        private static int ReadPositiveInt(string message)
        {
            while (true)
            {
                Console.Write(message);

                bool success = int.TryParse(
                    Console.ReadLine(),
                    out int result);

                if (!success)
                {
                    Console.WriteLine("Ошибка: Введите целое число.");
                    continue;
                }

                if (result <= 0)
                {
                    Console.WriteLine("Ошибка: Значение должно быть больше нуля.");
                    continue;
                }

                return result;
            }
        }

        /// <summary>
        /// Считывает год поступления на работу.
        /// </summary>
        /// <returns>
        /// Корректный год.
        /// </returns>
        private static int ReadEmploymentYear()
        {
            int currentYear = DateTime.Now.Year;

            while (true)
            {
                Console.Write("Введите год поступления на работу: ");

                bool success = int.TryParse(
                    Console.ReadLine(),
                    out int year);

                if (!success)
                {
                    Console.WriteLine("Ошибка: Введите корректный год.");
                    continue;
                }

                if (year < 1950 || year > currentYear)
                {
                    Console.WriteLine($"Ошибка: Год должен быть между 1950 и {currentYear}.");
                    continue;
                }

                return year;
            }
        }

        /// <summary>
        /// Считывает количество отработанных дней.
        /// </summary>
        /// <param name="workingDays">
        /// Количество рабочих дней.
        /// </param>
        /// <returns>
        /// Корректное значение.
        /// </returns>
        private static int ReadWorkedDays(int workingDays)
        {
            while (true)
            {
                Console.Write("Введите количество отработанных дней в месяце: ");

                bool success = int.TryParse(
                    Console.ReadLine(),
                    out int daysWorked);

                if (!success)
                {
                    Console.WriteLine("Ошибка: Введите целое число.");
                    continue;
                }

                if (daysWorked < 0)
                {
                    Console.WriteLine("Ошибка: Значение не может быть отрицательным.");
                    continue;
                }

                if (daysWorked > workingDays)
                {
                    Console.WriteLine("Ошибка: Отработанные дни не могут превышать рабочие дни.");
                    continue;
                }

                return daysWorked;
            }
        }
    }
}