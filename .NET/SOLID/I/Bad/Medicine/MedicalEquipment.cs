namespace Bad.Medicine;

/// <summary>
/// Медицинское оборудование.
/// </summary>
internal class MedicalEquipment : IMedicalEntity
{
    public void Diagnose()
    {
        throw new NotImplementedException();
    }

    public void Operate()
    {
        throw new NotImplementedException();
    }

    public void Calibrate()
    {
        Console.WriteLine("Оборудование откалибровано.");
    }

    public void PrescribeDrug()
    {
        throw new NotImplementedException();
    }
}