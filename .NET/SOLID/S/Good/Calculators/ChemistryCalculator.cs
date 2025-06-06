namespace Good.Calculators;

/// <summary>
///     Калькулятор для химических расчетов.
/// </summary>
internal class ChemistryCalculator
{
    private const double LightSpeed = 299_792_458;

    /// <summary>
    ///     Расчет энергии химической реакции.
    /// </summary>
    /// <param name="initialMass">Начальная масса вещества (в кг).</param>
    /// <param name="finalMass">Конечная масса вещества (в кг).</param>
    /// <returns>Энергия реакции (в Дж).</returns>
    public double CalculateReactionEnergy(double initialMass, double finalMass)
    {
        var massDeficit = initialMass - finalMass;

        return massDeficit * Math.Pow(LightSpeed, 2);
    }
}