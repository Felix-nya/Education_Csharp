using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Test.Classes
{
    internal class GeometryException : Exception
    {
        internal int[]? Parameters { get; private set; }
        internal GeometryException(string message) : base(message) { }
        internal GeometryException() { }
    }
    internal class TriangleException : GeometryException
    {
        
    }
    internal class QuadrangleException : GeometryException
    {

    }
    internal class CircleException : GeometryException
    {

    }

    internal class Triangle
    {
        internal Triangle(int a, int b, int c)
        {
            if (a + b < c || a + c < b || b + c < a) throw new TriangleException();
        }
    }

    internal class Quadrangle
    {
        internal Quadrangle(int a, int b, int c, int d)
        {
            int[] ints = { a, b, c, d };
            ints.Sort();
            if (ints[3] > ints[0] + ints[1] + ints[2]) throw new QuadrangleException();
        }
    }

    internal class Circle
    {
        internal Circle(int radius)
        {
            if (radius < 0) throw new CircleException();
        }
    }
}