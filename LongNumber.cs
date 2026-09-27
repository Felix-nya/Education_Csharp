using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class LongNumber
    {
        private long _value = 0;
        internal LongNumber(long value)
        {
            _value = value;
        }
        internal LongNumber(LongNumber other)
        {
            _value = other._value;
        }
        public static LongNumber operator +(LongNumber a, LongNumber b) => new LongNumber(a._value + b._value);
        public static LongNumber operator -(LongNumber a, LongNumber b) => new LongNumber(a._value - b._value);
        public static LongNumber operator *(LongNumber a, LongNumber b) => new LongNumber(a._value * b._value);
        public static LongNumber operator /(LongNumber a, LongNumber b) => new LongNumber(a._value / b._value);
        public override bool Equals(object obj) => obj is LongNumber other && _value == other._value;
        public static bool operator ==(LongNumber a, LongNumber b) => a.Equals(b);
        public static bool operator !=(LongNumber a, LongNumber b) => !a.Equals(b);
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString("N0");
        public static explicit operator int(LongNumber num) => (int)num._value;
        public static explicit operator long(LongNumber num) => num._value;
        public static explicit operator short(LongNumber num) => (short)num._value;
        public static explicit operator bool(LongNumber num) => num._value != 0;
        public static implicit operator LongNumber(int value) => new LongNumber(value);
        public static implicit operator LongNumber(long value) => new LongNumber(value);
        public static implicit operator LongNumber(short value) => new LongNumber(value);
        public static implicit operator LongNumber(bool value) => new LongNumber(value ? 1 : 0);

        internal static bool TryParse(string str, out LongNumber? result)
        {
            if (long.TryParse(str, out long parsed))
            {
                result = new LongNumber(parsed);
                return true;
            }
            result = null;
            return false;
        }
    }

    internal static class LongNumberExtensions
    {
        internal static LongNumber ToLongNumber(this string str)
        {
            if (LongNumber.TryParse(str, out LongNumber? result)) return result;
            throw new FormatException("Нельзя привести к LongNumber");
        }

        internal static LongNumber ToLongNumber(this StringBuilder sb) => sb.ToString().ToLongNumber();
    }
}
