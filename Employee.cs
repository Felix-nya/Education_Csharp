using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class Employee
    {
        internal const string CompanyName = "Name";
        internal static readonly DateTime CompanyFoundedDate;
        protected readonly int EmployeeId;
        protected string Name = string.Empty;
        protected decimal BaseSalary = 0;
        static Employee()
        {
            Console.WriteLine("Вызов ст. конструктора");
            CompanyFoundedDate = DateTime.Now;
        }
        internal Employee(int id, string name, decimal salary)
        {
            EmployeeId = id;
            Name = name;
            BaseSalary = salary;
        }
        internal virtual decimal CalculateSalary()
        {
            return BaseSalary;
        }
        internal string GetEmployeeInfo()
        {
            return $"Id: {EmployeeId}, имя: {Name}, базовая зарплата: {BaseSalary}";
        }
    }
    internal class Manager : Employee
    {
        private decimal Bonus = 0;
        internal Manager(int id, string name, decimal sal, decimal bon) : base(id, name, sal)
        {
            Bonus = bon;
        }
        internal override decimal CalculateSalary()
        {
            return BaseSalary + Bonus;
        }
    }
    internal class Developer : Employee
    {
        private int CompletedProjects = 0;
        private const decimal ProjectBonus = 10;
        internal Developer(int id, string name, decimal sal, int proj) : base(id, name, sal)
        {
            CompletedProjects = proj;
        }
        internal override decimal CalculateSalary()
        {
            return BaseSalary + ProjectBonus * CompletedProjects;
        }
    }
}
