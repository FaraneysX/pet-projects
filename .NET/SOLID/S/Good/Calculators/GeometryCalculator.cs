namespace Good.Calculators;

/// <summary>
///     Калькулятор для геометрических расчетов.
/// </summary>
internal class GeometryCalculator
{
    /// <summary>
    ///     Расчет площади треугольника.
    /// </summary>
    /// <param name="baseLength">Длина основания треугольника (в м).</param>
    /// <param name="height">Высота треугольника (в м).</param>
    /// <returns>Площадь треугольника (в м^2).</returns>
    public double CalculateTriangleArea(double baseLength, double height)
    {
        return 0.5 * baseLength * height;
    }
}