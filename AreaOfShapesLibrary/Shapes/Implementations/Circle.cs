using AreaOfShapes.Library.Extensions;
using AreaOfShapes.Library.Shapes.Abstract;

namespace AreaOfShapes.Library.Shapes.Implementations
{
    public class Circle : IShape
    {
        private double _radius;
        private double _precision;

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


        public double Radius
        {
            get => _radius;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Radius must be bon-negative");

                _radius = value;
            }

        }

        public Circle() : this(0) { }

        public Circle(double radius) : this(radius, 1e-5) { }

        public Circle(double radius, double precision) 
        { 
            Radius = radius;
            _precision = precision;
        }

        public double GetArea() => Math.PI * Math.Pow(Radius, 2);

        public override bool Equals(object? obj)
        {
            if (obj.GetType() != GetType())
                return false;

            Circle circle = (Circle)obj;

            double maxPrecision = Math.Max(_precision, circle._precision);

            return _radius.EqualsWithPrecision(circle.Radius, maxPrecision);
        }

        public override int GetHashCode() => _radius.GetHashCode();
    }
}
