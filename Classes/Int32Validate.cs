using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Test.Classes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    internal class ValidateInt32Attribute : Attribute
    {
        internal int MinValue { get; }
        internal int MaxValue { get; }
        internal bool ZeroEnabled { get; }
        internal ValidateInt32Attribute(int min, int max, bool zeroEnabled)
        {
            MinValue = min; MaxValue = max; ZeroEnabled = zeroEnabled;
        }
    }

    internal class InvalidValueException : Exception
    {
        internal string Name { get; }
        internal string[] ListOfExceptions { get; }
        internal object CurrentValue { get; }
        //internal string Message { get; }
        internal InvalidValueException(string name, string[] listOfExceptions, object currentValue, string message)
            : base($"{name} - {currentValue} - {message}")
        {
            Name = name;
            ListOfExceptions = listOfExceptions;
            CurrentValue = currentValue;
            //Message = message;
        }
    }

    internal class Int33
    {
        [ValidateInt32(-10, 10, false)]
        public int first;
        [ValidateInt32(-10, 10, false)]
        public int second;
        [ValidateInt32(-10, 10, true)]
        public int FirstProp { get; }
        [ValidateInt32(-10, 10, true)]
        public int SecondProp { get; }
        internal Int33(int fir, int sec, int prfr, int scpr)
        {
            first = fir; second = sec; FirstProp = prfr; SecondProp = scpr;
        }
    }

    internal static class Int32Validate
    {
        internal static void Validate(Object obj)
        {
            var objType = obj.GetType();
            var properties = objType.GetProperties();
            var fields = objType.GetFields();
            foreach (var field in fields)
            {
                var atr = field.GetCustomAttribute<ValidateInt32Attribute>();
                int value = (int)field.GetValue(obj);
                if (value < atr.MinValue) 
                    throw new InvalidValueException(field.Name, [atr.MinValue.ToString(), atr.MaxValue.ToString(), atr.ZeroEnabled.ToString()], value, "Меньше минимального");
                if (value > atr.MaxValue) 
                    throw new InvalidValueException(field.Name, [atr.MinValue.ToString(), atr.MaxValue.ToString(), atr.ZeroEnabled.ToString()], value, "Больше максимального");
                if (value == 0 && !atr.ZeroEnabled) 
                    throw new InvalidValueException(field.Name, [atr.MinValue.ToString(), atr.MaxValue.ToString(), atr.ZeroEnabled.ToString()], value, "Ноль");
            }
            foreach (var property in properties)
            {
                var atr = property.GetCustomAttribute<ValidateInt32Attribute>();
                int value = (int)property.GetValue(obj);
                if (value < atr.MinValue) 
                    throw new InvalidValueException(property.Name, [atr.MinValue.ToString(), atr.MaxValue.ToString(), atr.ZeroEnabled.ToString()], value, "Меньше минимального"); ;
                if (value > atr.MaxValue) 
                    throw new InvalidValueException(property.Name, [atr.MinValue.ToString(), atr.MaxValue.ToString(), atr.ZeroEnabled.ToString()], value, "Больше максимального"); ;
                if (value == 0 && !atr.ZeroEnabled) 
                    throw new InvalidValueException(property.Name, [atr.MinValue.ToString(), atr.MaxValue.ToString(), atr.ZeroEnabled.ToString()], value, "Ноль"); ;
            }
        }
    }
}
