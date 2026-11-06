namespace Test.Classes
{
    internal class GeometryException : Exception
    {
        internal int[] Parameters { get; private set; }
        internal GeometryException(string message, int[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
    internal class TriangleException : GeometryException
    {
        internal TriangleException(string message, int[] parameters) : base(message, parameters) { }
    }
    internal class QuadrangleException : GeometryException
    {
        internal QuadrangleException(string message, int[] parameters) : base(message, parameters) { }
    }
    internal class CircleException : GeometryException
    {
        internal CircleException(string message, int[] parameters) : base(message, parameters) { }
    }

    internal class Triangle
    {
        internal Triangle(int a, int b, int c)
        {
            if (a + b <= c || a + c <= b || b + c <= a) throw new TriangleException("Треугольник с такими параметрами не существует", [a, b, c]);
        }
    }

    internal class Quadrangle
    {
        internal Quadrangle(int a, int b, int c, int d)
        {
            int[] ints = [a, b, c, d];
            if (ints.Max() >= ints.Sum() - ints.Max()) throw new QuadrangleException("Прямогульник с такими параметрами не существует", ints);
        }
    }

    internal class Circle
    {
        internal Circle(int radius)
        {
            if (radius <= 0) throw new CircleException("Окружность с таким параметром не существует", [radius]);
        }
    }
}