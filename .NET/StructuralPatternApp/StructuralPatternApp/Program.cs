// Денисов Никита Викторович
// 4 курс, 9 группа, ПММ

// Лабораторная работа #2
//
// Тема 10
// География, геология и метеорология, полезные ископаемые, водные ресурсы
// Структурный шаблон (Компоновщик (4+ уровня иерархии))

using StructuralPatternApp.Level1;
using StructuralPatternApp.Level2;
using StructuralPatternApp.Level3;
using StructuralPatternApp.Level4;

var europe = new Continent("Европа");

var russia = new Country("Россия");
var belarus = new Country("Беларусь");

var voronezh = new GeoRegion("Воронеж", 18.5);
var minsk = new GeoRegion("Минск", 23.1);

voronezh.Add(new MineralDeposit("Уголь", 25000));
voronezh.Add(new MineralDeposit("Олово", 1200));

minsk.Add(new MineralDeposit("Кобальт", 3500));
minsk.Add(new MineralDeposit("Медь", 15000));
minsk.Add(new WaterBody("Река", 850));

russia.Add(voronezh);
belarus.Add(minsk);

europe.Add(russia);
europe.Add(belarus);

Console.WriteLine("Структура континента:");
europe.DisplayDetails(0);
        
Console.WriteLine("\nВсего угля: " + 
                  europe.GetTotalResources("Уголь"));
        
Console.WriteLine("Всего водных ресурсов: " + 
                  europe.GetTotalResources("Олово"));