using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class User
    {
        internal string Email { get; set; } = string.Empty;
        private int _age = 0;
        internal int Age
        {
            get => _age; set
            {
                if (value < 0 || value > 150) throw new ArgumentException("Неверный возвраст");
                _age = value;
            }
        }
    }
}
