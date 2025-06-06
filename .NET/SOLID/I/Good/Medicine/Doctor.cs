namespace Good.Medicine;

/// <summary>
/// Доктор.
/// </summary>
internal class Doctor : IOperable, IPrescribable
{
    public void Operate()
    {
        Console.WriteLine("Врач проводит операцию.");
    }
    
    public void PrescribeDrug()
    {
        Console.WriteLine("Врач назначает лекарство.");
    }
}