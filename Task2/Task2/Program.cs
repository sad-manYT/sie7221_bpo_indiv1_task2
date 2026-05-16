using System;

namespace Task2_Payment
{
    /// <summary>
    /// Главный класс программы для расчета заработной платы
    /// </summary>
    class Program
    {
        /// <summary>
        /// Главный метод программы
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ПРОГРАММА ДЛЯ РАСЧЕТА ЗАРПЛАТЫ ===\n");

            try
            {
                // Ввод данных с клавиатуры
                Console.Write("Введите фамилию, имя, отчество: ");
                string fullName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    Console.WriteLine("Ошибка: ФИО не может быть пустым!");
                    return;
                }

                Console.Write("Введите оклад (в рублях): ");
                double salary = double.Parse(Console.ReadLine());

                Console.Write("Введите год поступления на работу: ");
                int yearOfEmployment = int.Parse(Console.ReadLine());

                // Проверка корректности года
                int currentYear = DateTime.Now.Year;
                if (yearOfEmployment < 1900 || yearOfEmployment > currentYear)
                {
                    Console.WriteLine($"Ошибка: Год поступления должен быть между 1900 и {currentYear}");
                    return;
                }

                Console.Write("Введите процент надбавки: ");
                double bonusPercent = double.Parse(Console.ReadLine());

                Console.Write("Введите подоходный налог (%): ");
                double incomeTax = double.Parse(Console.ReadLine());

                Console.Write("Введите количество отработанных дней в месяце: ");
                int daysWorked = int.Parse(Console.ReadLine());

                Console.Write("Введите количество рабочих дней в месяце: ");
                int workingDaysInMonth = int.Parse(Console.ReadLine());

                // Проверка корректности введенных дней
                if (daysWorked < 0 || daysWorked > workingDaysInMonth)
                {
                    Console.WriteLine("Ошибка: Отработанные дни не могут быть отрицательными или превышать рабочие дни!");
                    return;
                }

                if (workingDaysInMonth <= 0)
                {
                    Console.WriteLine("Ошибка: Количество рабочих дней должно быть положительным числом!");
                    return;
                }

                // Создание объекта класса Payment
                Payment employee = new Payment(fullName, salary, yearOfEmployment,
                                                bonusPercent, incomeTax, daysWorked, workingDaysInMonth);

                // Вычисление всех сумм
                employee.CalculateAccruedAmount();
                employee.CalculateWithheldAmount();

                // Вывод результатов
                employee.DisplayInfo();
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Неверный формат ввода! Пожалуйста, введите числовые значения правильно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}