using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class SmartTimer
    {
        internal int Seconds {  get; set; }
        internal int Minutes { get => Seconds / 60; }
        internal int Hours { get => Minutes / 60; }
    }
}
