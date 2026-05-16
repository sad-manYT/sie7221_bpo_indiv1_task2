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
        /// Конструктор класса Payment
        /// </summary>
        /// <param name="fullName">Фамилия, имя, отчество сотрудника</param>
        /// <param name="salary">Оклад сотрудника в рублях</param>
        /// <param name="yearOfEmployment">Год поступления на работу</param>
        /// <param name="bonusPercent">Процент надбавки (в процентах)</param>
        /// <param name="incomeTax">Процент подоходного налога (в процентах)</param>
        /// <param name="daysWorked">Количество отработанных дней в месяце</param>
        /// <param name="workingDaysInMonth">Количество рабочих дней в месяце</param>
        public Payment(string fullName, double salary, int yearOfEmployment,
                       double bonusPercent, double incomeTax, int daysWorked, int workingDaysInMonth)
        {
            this.fullName = fullName;
            this.salary = salary;
            this.yearOfEmployment = yearOfEmployment;
            this.bonusPercent = bonusPercent;
            this.incomeTax = incomeTax;
            this.daysWorked = daysWorked;
            this.workingDaysInMonth = workingDaysInMonth;
            this.accruedAmount = 0;
            this.withheldAmount = 0;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Возвращает начисленную сумму
        /// </summary>
        public double AccruedAmount => accruedAmount;

        /// <summary>
        /// Возвращает удержанную сумму
        /// </summary>
        public double WithheldAmount => withheldAmount;

        /// <summary>
        /// Возвращает ФИО сотрудника
        /// </summary>
        public string FullName => fullName;

        #endregion

        #region Публичные методы

        /// <summary>
        /// Вычисляет стаж сотрудника
        /// </summary>
        /// <returns>Полное количество лет, прошедших от года поступления до текущего года</returns>
        public int CalculateExperience()
        {
            int currentYear = DateTime.Now.Year;
            return currentYear - yearOfEmployment;
        }

        /// <summary>
        /// Вычисляет начисленную сумму (оклад за отработанные дни + надбавка)
        /// </summary>
        /// <returns>Начисленная сумма за месяц</returns>
        public double CalculateAccruedAmount()
        {
            // Дневная ставка = оклад / количество рабочих дней в месяце
            double dailyRate = salary / workingDaysInMonth;

            // Начисление за отработанные дни
            double baseAmount = dailyRate * daysWorked;

            // Надбавка
            double bonus = baseAmount * (bonusPercent / 100);

            // Общая начисленная сумма
            accruedAmount = baseAmount + bonus;

            return accruedAmount;
        }

        /// <summary>
        /// Вычисляет удержанную сумму (пенсионный фонд 1% + подоходный налог 13%)
        /// </summary>
        /// <returns>Удержанная сумма за месяц</returns>
        public double CalculateWithheldAmount()
        {
            // Если начисленная сумма еще не рассчитана, рассчитываем её
            if (accruedAmount == 0)
            {
                CalculateAccruedAmount();
            }

            // Отчисления в пенсионный фонд (1% от начисленной суммы)
            double pensionContribution = accruedAmount * 0.01;

            // Подоходный налог (13% от начисленной суммы)
            double taxAmount = accruedAmount * (incomeTax / 100);

            // Общая удержанная сумма
            withheldAmount = pensionContribution + taxAmount;

            return withheldAmount;
        }

        /// <summary>
        /// Вычисляет сумму, выдаваемую на руки (начисленная сумма минус удержанная)
        /// </summary>
        /// <returns>Сумма к выдаче на руки</returns>
        public double CalculateNetAmount()
        {
            // Если суммы еще не рассчитаны, рассчитываем их
            if (accruedAmount == 0)
            {
                CalculateAccruedAmount();
            }

            if (withheldAmount == 0)
            {
                CalculateWithheldAmount();
            }

            return accruedAmount - withheldAmount;
        }

        /// <summary>
        /// Выводит полную информацию о сотруднике и расчетах на консоль
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