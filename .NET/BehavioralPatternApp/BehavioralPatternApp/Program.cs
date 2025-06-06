using BehavioralPatternApp;
using BehavioralPatternApp.Commands;
using BehavioralPatternApp.Receivers;

var engine = new EngineSystem();
var weapons = new WeaponSystem();
var control = new StarshipControl();

while (true)
{
    Console.WriteLine("===Управление космическим кораблем===\n" +
                      "1. Установить мощность двигателя\n" +
                      "2. Включить/выключить оружие\n" +
                      "3. Зарядить оружие\n" +
                      "4. Отменить последнюю команду\n" +
                      "5. Посмотреть информацию\n" +
                      "6. Выйти\n" +
                      "Выберите действие: ");
    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Console.WriteLine("Введите значение от 0 до 100: ");

            if (int.TryParse(Console.ReadLine(), out var power))
            {
                control.ExecuteCommand(new EnginePowerCommand(engine, power));
            }

            break;
        case "2":
            Console.WriteLine("Активировать оружие? (Y/N): ");
            
            var answer = Console.ReadLine()?.Trim().ToUpper();

            if (answer is "Y" or "N")
            {
                control.ExecuteCommand(new PhaserCommand(weapons, answer == "Y"));
            }

            break;
        case "3":
            Console.WriteLine("Введите количество патронов в оружие: ");

            if (int.TryParse(Console.ReadLine(), out var charge))
            {
                control.ExecuteCommand(new PlasmaChargeCommand(weapons, charge));
            }

            break;
        case "4":
            control.UndoLastCommand();
            Console.WriteLine("Возврат к предыдущему состоянию.");
            break;
        case "5":
            Console.WriteLine("\n=== Текущий статус ===");
            Console.WriteLine($"Мощность двигателя: {engine.GetCurrentPower()}%");
            Console.WriteLine($"Состояние оружия: {weapons.IsPhaserActive()}");
            Console.WriteLine($"Патронов в оружии: {weapons.GetPlasmaCharge()}");
            break;
        case "6":
            break;
    }
}