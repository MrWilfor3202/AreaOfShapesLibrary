using AreaOfShapes.Library.Shapes.Implementations;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace AreaOfShapes.Tests;

[TestFixture]
internal class CircleTests
{
    private class CustomCircle : Circle
    {
        public CustomCircle()
        {
        }

        public CustomCircle(double radius) : base(radius)
        {
        }
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(0)]
    public void CreatetCircleWithIntegerRadiusTest(double radius)
    {
        Circle circle = new Circle(radius);

        ClassicAssert.AreEqual(circle.Radius, radius, 1e-5);
    }

    [TestCase(1.25)]
    [TestCase(5.65868)]
    [TestCase(7.7897987)]
    [TestCase(8.464456455)]
    [TestCase(0.000007)]
    [TestCase(5.00000)]
    public void CreatetCircleWithDoubleRadiusTest(double radius)
    {
        Circle circle = new Circle(radius);

        ClassicAssert.AreEqual(circle.Radius, radius, 1e-5);
    }

    [Test]
    public void TestCircleWithoutArgs()
    {
        Circle circle = new Circle();

        ClassicAssert.AreEqual(circle.Radius, 0, 1e-5);
        ClassicAssert.AreEqual(circle.Precision, 1e-5);
    }


    [TestCase(-5)]
    [TestCase(-1.434)]
    [TestCase(-0.00000001)]
    [TestCase(double.NegativeInfinity)]
    public void CreateCircleWithIncorrectSidesTest(double radius) => Assert.Throws<ArgumentException>(() => new Circle(radius));


    [TestCase(54, 9160.88417)]
    [TestCase(2, 12.56637)]
    [TestCase(3.25, 33.18307)]
    [TestCase(6.4354, 130.10709)]
    [TestCase(7.3232, 168.481283)]
    [TestCase(8, 201.06192)]
    [TestCase(10, 314.15926535)]
    [TestCase(16.343, 839.099445)]
    [TestCase(0, 0)]
    public void TestGetArea(double radius, double expected)
    {
        Circle circle = new Circle(radius);
        double area = circle.GetArea();

        ClassicAssert.AreEqual(expected, area, circle.Precision);
    }

    private static (Circle, Circle, bool)[] GetCirclesForComparison() => new (Circle, Circle, bool)[]
    {
        (new Circle(5), new Circle(5), true),
        (new Circle(1.5), new CustomCircle(1.5), false),
        (new Circle(7), new CustomCircle(5), false),
        (new Circle(), new CustomCircle(), false),
        (new Circle(), new Circle(), true),
        (new Circle(10), new Circle(10), true),
    };

    [Test]
    public void TestEqualsMethod()
    {
        var examples = GetCirclesForComparison();

        foreach (var example in examples)
        {
            bool circlesEquality = example.Item1.Equals(example.Item2);
            ClassicAssert.AreEqual(example.Item3, circlesEquality);
        }

    }
}

