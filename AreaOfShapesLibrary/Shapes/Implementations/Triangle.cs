using AreaOfShapes.Library.Extensions;
using AreaOfShapes.Library.Shapes.Abstract;
using System.Xml.Linq;

namespace AreaOfShapes.Library.Shapes.Implementations
{
    public class Triangle : IShape
    {
        public double Precision
        {
            get
            {
                return _precision;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("precision must me greater than 0");


                _precision = value;
            }
        }


        public double SideA
        {
            get
            {
                return _sideA;
            }
            set
            {
                ThrowExceptionIfTriangleDoesNotExist(value, SideB, SideC);
                _sideA = value;
            }
        }

        public double SideB
        {
            get
            {
                return _sideB;
            }
            set
            {
                ThrowExceptionIfTriangleDoesNotExist(SideA, value, SideC);
                _sideB = value;
            }
        }

        public double SideC
        {
            get
            {
                return _sideC;
            }
            set
            {
                ThrowExceptionIfTriangleDoesNotExist(SideA, SideB, value);
                _sideC = value;
            }
        }

        private double _sideA;
        private double _sideB;
        private double _sideC;
        private double _precision;

        public bool IsTriangleRight
        {
            get
            {
                var maxSide = Math.Max(SideA, Math.Max(SideB, SideC));
                var minSide = Math.Min(SideA, Math.Min(SideB, SideC));
                var midSide = SideA + SideB + SideC - (maxSide + minSide);

                return Math.Pow(maxSide, 2).EqualsWithPrecision(Math.Pow(minSide, 2) + Math.Pow(midSide, 2), _precision);
            }
        }

        public Triangle() : this(1, 1, 1) { }

        public Triangle(double sideA, double sideB, double sideC) : this(sideA, sideB, sideC, 1e-5) {}

        public Triangle(double sideA, double sideB, double sideC, double precision)
        {
            ThrowExceptionIfTriangleDoesNotExist(sideA, sideB, sideC);

            _sideA = sideA;
            _sideB = sideB;
            _sideC = sideC;
            _precision = precision;

        }

        public double GetArea()
        {
            var p = (SideA + SideB + SideC) / 2;

            return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
        }

        private void ThrowExceptionIfTriangleDoesNotExist(double a, double b, double c)
        {
            if (a < 0 || b < 0 || c < 0)
                throw new ArgumentException("All sides of triangle must be non-negative");

            if (a > b + c || b > a + c || c > a + b)
                throw new ArgumentException("Triangle inequality violated");
        }


        public override bool Equals(object? obj)
        {
            if (obj?.GetType() != GetType())
                return false;

            Triangle another = (Triangle)obj;

            double maxPrecision = Math.Max(_precision, another._precision);

            return SideA.EqualsWithPrecision(another.SideA, maxPrecision)
                && SideB.EqualsWithPrecision(another.SideB, maxPrecision)
                && SideC.EqualsWithPrecision(another.SideC, maxPrecision);
        }

        public override int GetHashCode()
            => unchecked((SideA.GetHashCode() * 397 ^ SideB.GetHashCode()) * 397 ^ SideC.GetHashCode());
    }
}