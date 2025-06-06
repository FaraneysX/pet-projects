namespace Bad.Medicine;

/// <summary>
/// Пациент.
/// </summary>
internal class Patient : IMedicalEntity
{
    public void Diagnose()
    {
        Console.WriteLine("Пациент проходит диагностику.");
    }

    public void Operate()
    {
        throw new NotImplementedException();
    }

    public void Calibrate()
    {
        throw new NotImplementedException();
    }

    public void PrescribeDrug()
    {
        throw new NotImplementedException();
    }
}