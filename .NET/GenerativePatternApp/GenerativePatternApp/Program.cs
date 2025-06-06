// Денисов Никита Викторович
// 4 курс, 9 группа, ПММ

// Лабораторная работа #1
//
// Тема 1
// Грузоперевозки и логистика (грузы, порты, склады, города, поезда, контейнеры...)
// Порождающий шаблон (Абстрактная фабрика (порождает 2+ подсистемы))

using GenerativePatternApp.Domain;
using GenerativePatternApp.Features.Air;
using GenerativePatternApp.Features.Road;
using GenerativePatternApp.Features.Sea;

var factories = new Dictionary<string, ILogisticsFactory>
{
    ["1"] = new RoadLogisticsFactory(),
    ["2"] = new SeaLogisticsFactory(),
    ["3"] = new AirLogisticsFactory()
};

while (true)
{
    Console.WriteLine("Выберите тип перевозки:\n" +
                      "1) Наземная\n" +
                      "2) Морская\n" +
                      "3) Воздушная\n" +
                      "4) Выход\n");

    var choice = Console.ReadLine();

    if (choice is null or "4") return;

    if (!factories.TryGetValue(choice, out var factory)) continue;

    var report = new LogisticsReport(factory);

    Console.WriteLine(report);
}