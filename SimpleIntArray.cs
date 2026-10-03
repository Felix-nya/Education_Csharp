using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class SimpleIntArray
    {
        private int[] array = new int[10];
        internal int this[int idx]
        {
            get
            {
                return array[idx];
            }
            set
            {
                if(value < 0 || value > array.Length) throw new IndexOutOfRangeException();
                array[idx] = value;
            }
        }
    }
}
