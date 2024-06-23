namespace AreaOfShapes.Library.Extensions
{
    internal static class DoubleExtensions
    {
        internal static bool EqualsWithPrecision(this double instance, double another, double precision)
            => Math.Abs(instance - another) < precision;
    }
}
