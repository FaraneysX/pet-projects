namespace Good.Calculators;

/// <summary>
///     Расчет силы.
/// </summary>
internal interface IForceCalculator
{
    /// <summary>
    ///     Расчет силы.
    /// </summary>
    /// <param name="mass">Масса.</param>
    /// <param name="acceleration">Ускорение.</param>
    /// <returns>Сила.</returns>
    double CalculateForce(double mass, double acceleration);
}