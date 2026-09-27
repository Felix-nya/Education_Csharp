using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class SmartNumber
    {
        private double _value = 1d;
        internal SmartNumber(double val)
        {
            _value = val;
        }
        public static SmartNumber operator +(SmartNumber a, SmartNumber b) => new SmartNumber(a._value + b._value);
        public static SmartNumber operator -(SmartNumber a, SmartNumber b) => new SmartNumber(a._value - b._value);
        public static SmartNumber operator *(SmartNumber a, SmartNumber b) => new SmartNumber(a._value * b._value);
        public static SmartNumber operator /(SmartNumber a, SmartNumber b) => new SmartNumber(a._value / b._value);
        public override bool Equals(object obj) => obj is SmartNumber other && _value == other._value;
        public static bool operator ==(SmartNumber a, SmartNumber b) => a.Equals(b);
        public static bool operator !=(SmartNumber a, SmartNumber b) => !a.Equals(b);
        public override int GetHashCode() => _value.GetHashCode();
        public static implicit operator SmartNumber(int val) => new SmartNumber(val);
        public static implicit operator SmartNumber(double val) => new SmartNumber(val);
        public static explicit operator int(SmartNumber num) => (int)num._value;
        public static explicit operator double(SmartNumber num) => num._value;
        public static explicit operator decimal(SmartNumber num) => (decimal)num._value;
        public static explicit operator SmartNumber(string num) => new SmartNumber(double.Parse(num));
        public static implicit operator string(SmartNumber num) => num._value.ToString();
        public override string ToString() => _value.ToString();
    }
}
