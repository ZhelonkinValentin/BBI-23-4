using System;
using System.Linq;

public struct Triangle
{
    public double[] Sides { get; private set; }

    public Triangle(double[] sides)
    {
        if (sides.Length != 3)
        {
            throw new ArgumentException("");
        }

        Sides = sides;
    }

    public string GetTriangleType()
    {
        if (Sides[0] == Sides[1] && Sides[1] == Sides[2])
        {
            return "Равносторонний";
        }
        else if (Sides[0] == Sides[1] || Sides[1] == Sides[2] || Sides[0] == Sides[2])
        {
            return "Равнобедренный";
        }
        else
        {
            return "Разносторонний";
        }
    }

    public double GetArea()
    {
        double p = (Sides[0] + Sides[1] + Sides[2]) / 2.0;
        return Math.Sqrt(p * (p - Sides[0]) * (p - Sides[1]) * (p - Sides[2]));
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Стороны: {Sides[0]}, {Sides[1]}, {Sides[2]}");
        Console.WriteLine($"Тип: {GetTriangleType()}");
        Console.WriteLine($"Площадь: {GetArea():F2}");
    }

    public static void Main(string[] args)
    {
        Triangle[] triangles = new Triangle[]
        {
                new Triangle(new double[] { 3, 4, 5 }),
                new Triangle(new double[] { 6, 6, 6 }),
                new Triangle(new double[] { 5, 5, 8 }),
                new Triangle(new double[] { 7, 10, 5 }),
                new Triangle(new double[] { 2, 2, 3 })
        };

        var sortedTriangles = triangles.OrderByDescending(t => t.GetArea()).ToArray();

        Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15}", "Треугольник", "Сторона 1", "Сторона 2", "Сторона 3", "Площадь");
        for (int i = 0; i < sortedTriangles.Length; i++)
        {
            var t = sortedTriangles[i];
            Console.WriteLine("{0,-15} {1,-15:F2} {2,-15:F2} {3,-15:F2} {4,-15:F2}",
                              $"Треугольник {i + 1}", t.Sides[0], t.Sides[1], t.Sides[2], t.GetArea());
        }
    }
}





