using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void TestingPoint(Point point)
        {
            point.X = 0;
            point.Y = 0;
        }
        static void TestingBox(Box box)
        {
            box.Width = 0;
            box.Height = 0;
        }
        static void Main(string[] args)
        {
            Point p1 = new Point();
            p1.ChangeCoords(10, 20);
            p1.PrintPoint();
            TestingPoint(p1);
            p1.PrintPoint();

            Box b1 = new Box();
            b1.ChangeSize(1, 2);
            b1.PrintBox();
            TestingBox(b1);
            b1.PrintBox();
        }
    }
}
