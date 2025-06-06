namespace BehavioralPatternApp.Receivers;

public class WeaponSystem
{
    private bool _isPhaserActive;
    private int _plasmaCharge;

    public void TogglePhaser(bool activate)
    {
        _isPhaserActive = activate;
    }

    public void ChargePlasma(int energyUnits)
    {
        if (energyUnits <= 0)
        {
            Console.WriteLine("[ОРУЖИЕ]: Патронов должно быть больше 0.");
            
            return;
        }
        
        _plasmaCharge = energyUnits;
        
        Console.WriteLine($"[ОРУЖИЕ]: Заряжено на {_plasmaCharge} единиц.");
    }
    
    public int GetPlasmaCharge() => _plasmaCharge;
    public bool IsPhaserActive() => _isPhaserActive;
}