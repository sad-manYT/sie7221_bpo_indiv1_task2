using System;

namespace Task2_Payment
{
    /// <summary>
    /// Класс для расчета заработной платы сотрудника
    /// </summary>
    public class Payment
    {
        #region Поля класса

        private string fullName;           // фамилия-имя-отчество
        private double salary;              // оклад
        private int yearOfEmployment;       // год поступления на работу
        private double bonusPercent;        // процент надбавки
        private double incomeTax;           // подоходный налог (в процентах)
        private int daysWorked;             // количество отработанных дней в месяце
        private int workingDaysInMonth;     // количество рабочих дней в месяце
        private double accruedAmount;       // начисленная сумма
        private double withheldAmount;      // удержанная сумма

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор класса Payment.
        /// </summary>
        /// <param name="fullName">
        /// Фамилия, имя, отчество сотрудника.
        /// </param>
        /// <param name="salary">
        /// Оклад сотрудника.
        /// </param>
        /// <param name="yearOfEmployment">
        /// Год поступления на работу.
        /// </param>
        /// <param name="bonusPercent">
        /// Процент надбавки.
        /// </param>
        /// <param name="incomeTax">
        /// Подоходный налог.
        /// </param>
        /// <param name="daysWorked">
        /// Количество отработанных дней.
        /// </param>
        /// <param name="workingDaysInMonth">
        /// Количество рабочих дней.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Возникает при некорректных данных.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Возникает при отрицательных значениях.
        /// </exception>
        public Payment(
            string fullName,
            double salary,
            int yearOfEmployment,
            double bonusPercent,
            double incomeTax,
            int daysWorked,
            int workingDaysInMonth)
        {
            ValidateInput(
                fullName,
                salary,
                yearOfEmployment,
                bonusPercent,
                incomeTax,
                daysWorked,
                workingDaysInMonth);

            this.fullName = fullName;
            this.salary = salary;
            this.yearOfEmployment = yearOfEmployment;
            this.bonusPercent = bonusPercent;
            this.incomeTax = incomeTax;
            this.daysWorked = daysWorked;
            this.workingDaysInMonth = workingDaysInMonth;

            accruedAmount = 0;
            withheldAmount = 0;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Возвращает начисленную сумму.
        /// </summary>
        public double AccruedAmount => accruedAmount;

        /// <summary>
        /// Возвращает удержанную сумму.
        /// </summary>
        public double WithheldAmount => withheldAmount;

        /// <summary>
        /// Возвращает ФИО сотрудника. Предполагается адекватный ввод от пользователя
        /// </summary>
        public string FullName => fullName;

        #endregion

        #region Методы проверки

        /// <summary>
        /// Проверяет корректность
        /// входных данных.
        /// </summary>
        private void ValidateInput(
            string fullName,
            double salary,
            int yearOfEmployment,
            double bonusPercent,
            double incomeTax,
            int daysWorked,
            int workingDaysInMonth)
        {
            int currentYear =
                DateTime.Now.Year;

            if (string.IsNullOrWhiteSpace(
                fullName))
            {
                throw new ArgumentException("ФИО не может быть пустым.");
            }

            if (salary <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(salary), "Оклад должен быть больше нуля.");
            }

            if (yearOfEmployment < 1950 || yearOfEmployment > currentYear)
            {
                throw new ArgumentOutOfRangeException(nameof(yearOfEmployment),
                    $"Год поступления " +
                    $"должен быть между " +
                    $"1950 и {currentYear}.");
            }

            if (bonusPercent < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bonusPercent),
                    "Процент надбавки " +
                    "не может быть отрицательным.");
            }

            if (incomeTax < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(incomeTax),
                    "Подоходный налог " +
                    "не может быть отрицательным.");
            }

            if (workingDaysInMonth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(workingDaysInMonth),
                    "Количество рабочих " +
                    "дней должно быть " +
                    "больше нуля.");
            }

            if (daysWorked < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(daysWorked),
                    "Количество " +
                    "отработанных дней " +
                    "не может быть отрицательным.");
            }

            if (daysWorked > workingDaysInMonth)
            {
                throw new ArgumentException(
                    "Отработанные дни " +
                    "не могут превышать " +
                    "рабочие дни.");
            }
        }

        #endregion

        #region Публичные методы

        /// <summary>
        /// Вычисляет стаж сотрудника.
        /// </summary>
        /// <returns>
        /// Полное количество лет стажа.
        /// </returns>
        public int CalculateExperience()
        {
            int currentYear = DateTime.Now.Year;

            return currentYear - yearOfEmployment;
        }

        /// <summary>
        /// Вычисляет начисленную сумму.
        /// </summary>
        /// <returns>
        /// Начисленная сумма.
        /// </returns>
        public double
            CalculateAccruedAmount()
        {
            double dailyRate = salary / workingDaysInMonth;

            double baseAmount = dailyRate * daysWorked;

            double bonus = baseAmount * (bonusPercent / 100);

            accruedAmount = baseAmount + bonus;

            return accruedAmount;
        }

        /// <summary>
        /// Вычисляет удержанную сумму.
        /// </summary>
        /// <returns>
        /// Удержанная сумма.
        /// </returns>
        public double
            CalculateWithheldAmount()
        {
            if (accruedAmount == 0)
            {
                CalculateAccruedAmount();
            }

            double pensionContribution = accruedAmount * 0.01;

            double taxAmount = accruedAmount * (incomeTax / 100);

            withheldAmount = pensionContribution + taxAmount;

            return withheldAmount;
        }

        /// <summary>
        /// Вычисляет сумму к выдаче.
        /// </summary>
        /// <returns>
        /// Сумма на руки.
        /// </returns>
        public double
            CalculateNetAmount()
        {
            if (accruedAmount == 0)
            {
                CalculateAccruedAmount();
            }

            if (withheldAmount == 0)
            {
                CalculateWithheldAmount();
            }

            return accruedAmount
                   - withheldAmount;
        }

        /// <summary>
        /// Выводит информацию
        /// о сотруднике.
        /// </summary>
        public void DisplayInfo()
        {
            Console.WriteLine("\n========== ИНФОРМАЦИЯ О СОТРУДНИКЕ ==========");
            Console.WriteLine($"ФИО: {fullName}");
            Console.WriteLine($"Оклад: {salary:F2} руб.");
            Console.WriteLine($"Год поступления на работу: {yearOfEmployment}");
            Console.WriteLine($"Процент надбавки: {bonusPercent}%");
            Console.WriteLine($"Подоходный налог: {incomeTax}%");
            Console.WriteLine($"Отработанные дни: {daysWorked}");
            Console.WriteLine($"Рабочие дни в месяце: {workingDaysInMonth}");
            Console.WriteLine($"Стаж: {CalculateExperience()} лет");
            Console.WriteLine($"Начисленная сумма: {accruedAmount:F2} руб.");
            Console.WriteLine($"Удержанная сумма: {withheldAmount:F2} руб.");
            Console.WriteLine($"Сумма к выдаче на руки: {CalculateNetAmount():F2} руб.");
            Console.WriteLine("=============================================\n");
        }

        #endregion
    }
}