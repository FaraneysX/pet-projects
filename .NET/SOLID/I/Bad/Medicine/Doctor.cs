namespace Bad.Medicine;

/// <summary>
/// Врач.
/// </summary>
internal class Doctor : IMedicalEntity
{
    public void Diagnose()
    {
        throw new NotImplementedException();
    }

    public void Operate()
    {
        Console.WriteLine("Врач проводит операцию.");
    }

    public void Calibrate()
    {
        throw new NotImplementedException();
    }

    public void PrescribeDrug()
    {
        Console.WriteLine("Врач назначает лекарство.");
    }
}