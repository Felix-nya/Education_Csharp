using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class Rectangle
    {
        internal required double Width { get; init; }
        internal required double Height { get; init; }
        internal double Area { get => Width * Height; }
    }
}
