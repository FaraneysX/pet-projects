using Good.Calculators;

namespace Good;

/// <summary>
///     Модуль физики.
/// </summary>
internal class PhysicsModule(IForceCalculator calculator)
{
    /// <summary>
    ///     Старт расчетов.
    /// </summary>
    public void Run()
    {
        var force = calculator.CalculateForce(10, 9.8);

        Console.WriteLine($"Рассчитанная сила: {force} Н");
    }
}