namespace Good.Calculators;

/// <summary>
///     Калькулятор для физических расчетов.
/// </summary>
internal class PhysicsCalculator
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
}