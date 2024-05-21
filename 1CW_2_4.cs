using System;
using System.Linq;

public abstract class Shape
{
    public abstract double GetArea();
    public abstract double GetPerimeter();
    public abstract string GetName();
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override double GetArea()
    {
        return Width * Height;
    }

    public override double GetPerimeter()
    {
        return 2 * (Width + Height);
    }

    public override string GetName()
    {
        return "Прямоугольник";
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * Math.Pow(Radius, 2);
    }

    public override double GetPerimeter()
    {
        return 2 * Math.PI * Radius;
    }

    public override string GetName()
    {
        return "Круг";
    }
}

public class Triangle : Shape
{
    public double[] Sides { get; private set; }

    public Triangle(double a, double b, double c)
    {
        Sides = new double[] { a, b, c };
    }

    public override double GetArea()
    {
        double p = GetPerimeter() / 2;
        return Math.Sqrt(p * (p - Sides[0]) * (p - Sides[1]) * (p - Sides[2]));
    }

    public override double GetPerimeter()
    {
        return Sides.Sum();
    }

    public override string GetName()
    {
        return "Треугольник";
    }
}class Program
{
    static void Main(string[] args)
    {
        Rectangle[] rectangles = new Rectangle[]
        {
            new Rectangle(3, 4),
            new Rectangle(5, 6),
            new Rectangle(2, 3),
            new Rectangle(7, 8),
            new Rectangle(4, 5)
        };

        Circle[] circles = new Circle[]
        {
            new Circle(3),
            new Circle(5),
            new Circle(2),
            new Circle(4),
            new Circle(6)
        };

        Triangle[] triangles = new Triangle[]
        {
            new Triangle(3, 4, 5),
            new Triangle(5, 5, 5),
            new Triangle(7, 10, 5),
            new Triangle(6, 8, 10),
            new Triangle(2, 2, 3)
        };

        PrintSortedShapes(rectangles);
        PrintSortedShapes(circles);
        PrintSortedShapes(triangles);

        var allShapes = rectangles.Cast<Shape>()
                                  .Concat(circles)
                                  .Concat(triangles)
                                  .OrderByDescending(s => s.GetArea())
                                  .ToArray();

        PrintShapesTable("Все фигуры", allShapes);
    }

    static void PrintSortedShapes(Shape[] shapes)
    {
        var sortedShapes = shapes.OrderByDescending(s => s.GetArea()).ToArray();
        PrintShapesTable(sortedShapes[0].GetName(), sortedShapes);
    }

    static void PrintShapesTable(string title, Shape[] shapes)
    {
        Console.WriteLine($"{title}:");
        Console.WriteLine("{0,-15} {1,-15} {2,-15}", "Название", "Периметр", "Площадь");
        Console.WriteLine(new string('-', 45));

        foreach (var shape in shapes)
        {
            Console.WriteLine("{0,-15} {1,-15:F2} {2,-15:F2}", shape.GetName(), shape.GetPerimeter(), shape.GetArea());
        }
        Console.WriteLine();
    }
}