using Good;
using Good.Calculators;

var standardCalculator = new StandardForceCalculator();
var physicsWithStandard = new PhysicsModule(standardCalculator);
physicsWithStandard.Run();

var frictionCalculator = new FrictionForceCalculator(0.2);
var physicsWithFriction = new PhysicsModule(frictionCalculator);
physicsWithFriction.Run();