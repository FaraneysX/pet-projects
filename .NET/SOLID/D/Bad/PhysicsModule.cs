namespace Bad;

/// <summary>
///     Модуль физики.
/// </summary>
internal class PhysicsModule
{
    /// <summary>
    ///     Калькулятор.
    /// </summary>
    private readonly ForceCalculator _calculator = new();

    /// <summary>
    ///     Старт расчетов.
    /// </summary>
    public void Run()
    {
        var force = _calculator.CalculateForce(10, 9.8);

        Console.WriteLine($"Рассчитанная сила: {force} Н.");
    }
}