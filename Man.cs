using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal abstract class Man
    {
        protected string _name = string.Empty;
        protected int _age;
        internal virtual void SetName(string name)
        {
            if (name is null) throw new ArgumentException("Null name");
            else _name = name;
        }
        internal virtual void SetAge(int age)
        {
            if (age < 0 || age > 300) throw new ArgumentException("Wrong age");
            else _age = age;
        }
        internal virtual void Describe()
        {
            Console.WriteLine($"Человек, {_name}, {_age}");
        }
    }
    internal class Teenager : Man
    {
        private string _school = string.Empty;
        internal Teenager(string name, string school, int age)
        {
            _name = name;
            _school = school;
            _age = age;
        }
        internal void SetSchool(string school)
        {
            if (school is null) throw new ArgumentException("Null school");
            else _school = school;
        }
        internal override void SetAge(int age)
        {
            if (age < 13 || age > 19) throw new ArgumentException("Wrong age for teenager");
            else _age = age;
        }
        internal override void Describe()
        {
            Console.WriteLine($"Подросток, {_name}, {_age}, Место учебы: {_school}");
        }
    }
    internal class Worker : Man
    {
        private string _workPlace = string.Empty;
        internal Worker(string name, string workPlace, int age)
        {
            _name = name;
            _age = age;
            _workPlace = workPlace;
        }
        internal void SetWorkPlace(string workPlace)
        {
            if (workPlace is null) throw new ArgumentException("Null workPlace");
            else _workPlace = workPlace;
        }
        internal override void SetAge(int age)
        {
            if (age < 16 || age > 70) throw new ArgumentException("Wrong age for worker");
            else _age = age;
        }
        internal override void Describe()
        {
            Console.WriteLine($"Работник, {_name}, {_age}, Место работы: {_workPlace}");
        }
    }
}
