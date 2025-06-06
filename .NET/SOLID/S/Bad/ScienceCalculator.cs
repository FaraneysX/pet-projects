namespace Bad;

/// <summary>
///     Калькулятор для расчетов.
/// </summary>
internal class ScienceCalculator
{
    /// <summary>
    ///     Расчет силы.
    /// </summary>
    /// <param name="mass">Масса тела (в киллограммах).</param>
    /// <param name="acceleration">Ускорение (в м/с^2).</param>
    /// <returns>Сила (в Ньютонах).</returns>
    public double CalculateForce(double mass, double acceleration)
    {
        return mass * acceleration;
    }

    /// <summary>
    ///     Расчет энергии химической реакции.
    /// </summary>
    /// <param name="initialMass">Начальная масса вещества (в кг).</param>
    /// <param name="finalMass">Конечная масса вещества (в кг).</param>
    /// <returns>Энергия реакции (в Дж).</returns>
    public double CalculateReactionEnergy(double initialMass, double finalMass)
    {
        const double lightSpeed = 299_792_458;
        var massDeficit = initialMass - finalMass;

        return massDeficit * Math.Pow(lightSpeed, 2);
    }

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