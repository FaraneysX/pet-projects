namespace Good.Medicine;

/// <summary>
/// Пациент.
/// </summary>
internal class Patient : IDiagnosable
{
    public void Diagnose()
    {
        Console.WriteLine("Пациент проходит диагностику.");
    }
}