using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        internal class Animal
        {
            protected string _name;
            internal Animal(string name) { _name = name; }
            internal virtual void MakeSound()
            {
                Console.WriteLine($"{_name} says eye");
            }
        }
        internal class Dog: Animal
        {
            internal Dog(string name): base(name) { }
            internal override void MakeSound() 
            {
                Console.WriteLine($"{_name} says gaff");
            }
        }
        internal class Cat : Animal
        {
            internal Cat(string name) : base(name) { }
            internal override void MakeSound()
            {
                Console.WriteLine($"{_name} says meow");
            }
        }
        internal static void Describe(Animal a)
        {
            Dog doggy = a as Dog;
            Cat kitten = a as Cat;
            if (doggy != null)
            {
                Console.WriteLine("It`s Doggy");
                doggy.MakeSound();
            }
            else if (kitten != null)
            {
                Console.WriteLine("It`s Kitten");
                kitten.MakeSound();
            }
            else
            {
                Console.WriteLine("I don`t now :(");
                a.MakeSound();
            }
        }
        static void Main(string[] args)
        {
            Animal rex = new Dog("Rex");
            Animal mysia = new Cat("Mysia");
            Animal bird = new Animal("Bird");
            Describe(rex);
            Describe(mysia);
            Describe(bird);
        }
    }
}
