using BehavioralPatternApp.Receivers;

namespace BehavioralPatternApp.Commands;

public class PlasmaChargeCommand(WeaponSystem weapons, int chargeAmount) : ICommand
{
    private int _previousCharge;

    public void Execute()
    {
        _previousCharge = weapons.GetPlasmaCharge();
        weapons.ChargePlasma(chargeAmount);
    }

    public void Undo() => weapons.ChargePlasma(_previousCharge);
}