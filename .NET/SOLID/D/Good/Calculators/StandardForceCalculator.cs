namespace Good.Calculators;

/// <summary>
///     Расчет силы стандартным способом.
/// </summary>
internal class StandardForceCalculator : IForceCalculator
{
    public double CalculateForce(double mass, double acceleration)
    {
        return mass * acceleration;
    }
}