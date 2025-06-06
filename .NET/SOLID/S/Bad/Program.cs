using Bad;

var scienceCalculator = new ScienceCalculator();

var force = scienceCalculator.CalculateForce(10, 9.8);
Console.WriteLine($"Сила: {force} H.");

var reactionEnergy = scienceCalculator.CalculateReactionEnergy(1, 0.9999);
Console.WriteLine($"Энергия реакции: {reactionEnergy} Дж.");

var triangleArea = scienceCalculator.CalculateTriangleArea(5, 10);
Console.WriteLine($"Площадь треугольника: {triangleArea} м^2");