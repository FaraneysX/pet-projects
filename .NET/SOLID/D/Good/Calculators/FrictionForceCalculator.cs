namespace Good.Calculators;

/// <summary>
///     Расчет силы с учетом трения.
/// </summary>
/// <param name="frictionCoefficient">Коэффициент трения.</param>
internal class FrictionForceCalculator(double frictionCoefficient) : IForceCalculator
{
    public double CalculateForce(double mass, double acceleration)
    {
        var normalForce = mass * 9.8;
        var frictionForce = frictionCoefficient * normalForce;

        return mass * acceleration - frictionForce;
    }
}