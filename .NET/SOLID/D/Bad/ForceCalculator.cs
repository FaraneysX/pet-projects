namespace Bad;

/// <summary>
///     Расчет силы.
/// </summary>
internal class ForceCalculator
{
    /// <summary>
    ///     Расчет силы.
    /// </summary>
    /// <param name="mass">Масса (в кг).</param>
    /// <param name="acceleration">Ускорение.</param>
    /// <returns>Сила.</returns>
    public double CalculateForce(double mass, double acceleration)
    {
        return mass * acceleration;
    }
}