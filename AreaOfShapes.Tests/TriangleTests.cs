using AreaOfShapes.Library.Shapes.Implementations;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace AreaOfShapesLibrary.Tests
{
    [TestFixture]
    public class TriangleTests
    {
        private class CustomTriangle : Triangle
        {
            public CustomTriangle(double sideA, double sideB, double sideC) : base(sideA, sideB, sideC)
            {
            }
        }

        [TestCase(1, 2, 3)]
        [TestCase(2, 2, 2)]
        [TestCase(5, 5, 3)]
        [TestCase(5, 4, 3)]
        [TestCase(5, 4, 3)]
        public void CreatetTriangleWithIntegerSidesTest(double a, double b, double c)
        {
            Triangle triangle = new Triangle(a, b, c);
            ClassicAssert.AreEqual(triangle.SideA, a, 1e-5);
            ClassicAssert.AreEqual(triangle.SideB, b, 1e-5);
            ClassicAssert.AreEqual(triangle.SideC, c, 1e-5);
        }     
        
        [TestCase(1.001, 2.5, 3.25)]
        [TestCase(2.34, 2.40, 1)]
        [TestCase(4, 5.25, 3.1)]
        [TestCase(5.62, 4.32, 3.554)]
        [TestCase(5.0, 4.0, 3.0)]
        public void CreatetTriangleWithDoubleSidesTest(double a, double b, double c)
        {
            Triangle triangle = new Triangle(a, b, c);
            ClassicAssert.AreEqual(triangle.SideA, a, 1e-5);
            ClassicAssert.AreEqual(triangle.SideB, b, 1e-5);
            ClassicAssert.AreEqual(triangle.SideC, c, 1e-5);
        }

        [Test]
        public void TestTriangleWithoutArgs() 
        {
            Triangle triangle = new Triangle();
            ClassicAssert.AreEqual(triangle.SideA, 1, 1e-5);
            ClassicAssert.AreEqual(triangle.SideB, 1, 1e-5);
            ClassicAssert.AreEqual(triangle.SideC, 1, 1e-5);
            ClassicAssert.AreEqual(triangle.Precision, 1e-5);
        }

        [TestCase(2, -1, 2)]
        [TestCase(-1, 1, 2)]
        [TestCase(3, 1, -2)]
        [TestCase(-3.2, -4.2, -5.5)]
        [TestCase(10, 5, 1)]
        [TestCase(2, 6, 3)]
        [TestCase(1, 1, 3)]
        [TestCase(3.25, 6.51, 3.25)]
        [TestCase(0, 1, 0)]
        public void CreateTriangleWithIncorrectSidesTest(double a, double b, double c) => Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));

        [TestCase(3, 4, 5, true)]
        [TestCase(3.0, 4.0, 5.0, true)]
        [TestCase(1, 2, 3, false)]
        [TestCase(9, 16, 25, true)]
        [TestCase(0, 0, 0, false)]
        [TestCase(7.3563, 8.6686, 9.57565, false)]
        [TestCase(3.6565, 8.6686, 9.5084040, true)]
        public void IsTriangleRightTests(double a, double b, double c, bool expected)
        {
            Triangle triangle = new Triangle(a, b, c);

            ClassicAssert.AreEqual(triangle.IsTriangleRight, triangle.IsTriangleRight);
        }

        [TestCase(3, 3, 1, 1.47902)]
        [TestCase(3, 4, 5, 6)]
        [TestCase(1, 4, 5, 0)]
        [TestCase(3, 4, 7, 0)]
        [TestCase(3, 4, 7, 0)]
        [TestCase(6, 4, 5, 9.921567)]
        [TestCase(10, 7, 5, 16.248077)]
        [TestCase(3, 7, 5, 6.495191)]
        [TestCase(6, 4, 8, 11.61895)]
        [TestCase(9, 11, 15, 49.164901)]
        [TestCase(20, 10, 15, 72.618438)]
        [TestCase(12.5, 9.1, 6, 25.64528)]
        [TestCase(3.22, 4.1, 6.2, 5.970488)]
        [TestCase(1.6, 4.1, 3.3, 2.502798)]
        [TestCase(0, 0, 0, 0)]
        public void TestGetArea(double a, double b, double c, double expected) 
        {
            Triangle triangle = new Triangle(a, b, c);
            double area = triangle.GetArea();

            ClassicAssert.AreEqual(expected, area, 1e-5);
        }

        [TestCase(5.25, 1, 6, 1e-5, 1.85293)]
        [TestCase(1.85293, 2, 3, 1e-5, 1.811063)]
        [TestCase(7.7767766, 8.772211, 6.434345354, 1e-6, 24.230687)]
        public void TestGetAreaWithCustomPrecision(double a, double b, double c, double precision, double expected)
        {
            Triangle triangle = new Triangle(a, b, c);
            double area = triangle.GetArea();

            ClassicAssert.AreEqual(expected, area, precision);
        }


        private static (Triangle, Triangle, bool)[] GetTrianglesForComparison() => new (Triangle, Triangle, bool)[]
        {
            (new Triangle(5, 4, 1.5), new Triangle(5, 4, 1.5), true),
            (new Triangle(4, 5, 1.5), new Triangle(5, 4, 1.5), false),
            (new Triangle(5, 4, 1.5), new CustomTriangle(5, 4, 1.5), false),
            (new Triangle(4, 5, 1.5), new CustomTriangle(5, 4, 1.5), false),
            (new Triangle(0, 0, 0), new Triangle(5, 4, 1.5), false),
            (new Triangle(5, 5, 0), new Triangle(5, 5, 0), true),
            (new Triangle(3, 1, 2), new Triangle(3, 1, 2), true)
        };

        [Test]
        public void TestEqualsMethod() 
        {
            var examples = GetTrianglesForComparison();

            foreach (var example in examples)
            {
                bool trianglesEquality = example.Item1.Equals(example.Item2);
                ClassicAssert.AreEqual(example.Item3, trianglesEquality);
            }

        }
    }
}
