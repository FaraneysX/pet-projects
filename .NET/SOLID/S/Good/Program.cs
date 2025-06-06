using Good.Calculators;

var physicsCalculator = new PhysicsCalculator();
var force = physicsCalculator.CalculateForce(10, 9.8);
Console.WriteLine($"Сила: {force} H.");

var chemistryCalculator = new ChemistryCalculator();
var reactionEnergy = chemistryCalculator.CalculateReactionEnergy(1, 0.9999);
Console.WriteLine($"Энергия реакции: {reactionEnergy} Дж.");

var geometryCalculator = new GeometryCalculator();
var triangleArea = geometryCalculator.CalculateTriangleArea(5, 10);
Console.WriteLine($"Площадь треугольника: {triangleArea} м^2");