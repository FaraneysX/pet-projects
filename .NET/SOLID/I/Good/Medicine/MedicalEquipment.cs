namespace Good.Medicine;

/// <summary>
/// Медицинское оборудование.
/// </summary>
internal class MedicalEquipment : ICalibratable
{
    public void Calibrate()
    {
        Console.WriteLine("Оборудование откалибровано.");
    }
}