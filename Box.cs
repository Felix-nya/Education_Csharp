using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal struct Point
    {
        internal int X,Y;
        internal void ChangeCoords(int x, int y)
        {
            X = x;
            Y = y;
        }
        internal void PrintPoint()
        {
            Console.WriteLine("Точка: " + this.X.ToString() + "\t" + this.Y.ToString());
        }
    }
    internal class Box
    {
        internal int Width, Height;
        internal void ChangeSize(int w, int h)
        {
            Width = w;
            Height = h;
        }
        internal void PrintBox()
        {
            Console.WriteLine("Box: " + this.Width.ToString() + "\t" + this.Height.ToString());
        }
    }
}
