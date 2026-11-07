using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Classes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class NameAttribute : Attribute
    {
        internal string Descr { get; }
        internal NameAttribute(string name)
        {
            Descr = name;
        }
    }

    internal class FirstClass
    {
        [Name("Один")]
        public int First { get; set; } = 1;
        [Name("Два")]
        public int Second { get; set; } = 2;
        [Name("Три")]
        public int Three { get; set; } = 3;
        [Name("Четыре")]
        public int Four { get; set; } = 4;
        [Name("Пять")]
        public int Five { get; set; } = 5;
        [Name("Шесть")]
        public int Six { get; set; } = 6;
        [Name("Семь")]
        public int Seven { get; set; } = 7;
        [Name("Восемь")]
        public int Eight { get; set; } = 8;
        [Name("Девять")]
        public int Nine { get; set; } = 9;
        [Name("Десять")]
        public int Tehth { get; set; } = 10;
    }
}
