namespace AccidentesDeMadrid.Entity.Enum;

public record VehicleType(string Message)
{
    public static readonly VehicleType SamurAmbulance = new("Ambulancia SAMUR");
    public static readonly VehicleType Bus = new("Autobús");
    public static readonly VehicleType ArticulatedBus = new("Autobús articulado");
    public static readonly VehicleType EmtBus = new("Autobus EMT");
    public static readonly VehicleType Motorhome = new("Autocaravana");
    public static readonly VehicleType Bicycle = new("Bicicleta");
    public static readonly VehicleType EpacBicycle = new("Bicicleta EPAC (pedaleo asistido)");
    public static readonly VehicleType RigidTruck = new("Camión rígido");
    public static readonly VehicleType Cycle = new("Ciclo");
    public static readonly VehicleType MotorizedCycleL1eA = new("Ciclo de motor L1e-A");
    public static readonly VehicleType Moped = new("Ciclomotor");
    public static readonly VehicleType TwoWheelMopedL1eB = new("Ciclomotor de dos ruedas L1e-B");
    public static readonly VehicleType ThreeWheelMoped = new("Ciclomotor de tres ruedas");
    public static readonly VehicleType LightQuadricycle = new("Cuadriciclo ligero");
    public static readonly VehicleType HeavyQuadricycle = new("Cuadriciclo no ligero");
    public static readonly VehicleType Van = new("Furgoneta");
    public static readonly VehicleType ConstructionMachinery = new("Maquinaria de obras");
    public static readonly VehicleType ThreeWheelMotorcycleOver125cc = new("Moto de tres ruedas > 125cc");
    public static readonly VehicleType ThreeWheelMotorcycleUpTo125cc = new("Moto de tres ruedas hasta 125cc");
    public static readonly VehicleType MotorcycleOver125cc = new("Motocicleta > 125cc");
    public static readonly VehicleType MotorcycleUpTo125cc = new("Motocicleta hasta 125cc");
    public static readonly VehicleType OtherMotorVehicles = new("Otros vehículos con motor");
    public static readonly VehicleType OtherNonMotorVehicles = new("Otros vehículos sin motor");
    public static readonly VehicleType NonElectricScooter = new("Patinete no eléctrico");
    public static readonly VehicleType Trailer = new("Remolque");
    public static readonly VehicleType SemiTrailer = new("Semiremolque");
    public static readonly VehicleType Unspecified = new("Sin especificar");
    public static readonly VehicleType OffRoadVehicle = new("Todo terreno");
    public static readonly VehicleType TractorUnit = new("Tractocamión");
    public static readonly VehicleType PassengerCar = new("Turismo");
    public static readonly VehicleType ArticulatedVehicle = new("Vehículo articulado");
    public static readonly VehicleType ElectricPmv = new("VMU eléctrico");
}
