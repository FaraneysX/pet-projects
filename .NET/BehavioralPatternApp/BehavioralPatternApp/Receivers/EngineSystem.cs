namespace BehavioralPatternApp.Receivers;

public class EngineSystem
{
    private int _currentPower;

    public void SetPower(int percentage)
    {
        if (percentage is < 0 or > 100)
        {
            Console.Error.WriteLine("Процент должен быть от 0 до 100");
            
            return;
        }
        
        _currentPower = percentage;
        Console.WriteLine($"[ДВИГАТЕЛЬ]: Мощность установлена на {percentage}");
    }
    
    public int GetCurrentPower() => _currentPower;
}