namespace AccidentesDeMadrid.Entity.Enum;

public record AccidentType(string message)
{
    public static readonly AccidentType RearEndCollision = new("Alcance");
    public static readonly AccidentType AnimalStrike = new("Atropello a animal");
    public static readonly AccidentType PedestrianStrike = new("Atropello a persona");
    public static readonly AccidentType Fall = new("Caída");
    public static readonly AccidentType CollisionWithFixedObject = new("Choque contra obstáculo fijo");
    public static readonly AccidentType HeadOnCollision = new("Colisión frontal");
    public static readonly AccidentType AngleCollision = new("Colisión fronto-lateral");
    public static readonly AccidentType SideCollision = new("Colisión lateral");
    public static readonly AccidentType MultiVehicleCollision = new("Colisión múltiple");
    public static readonly AccidentType RoadDeparture = new("Solo salida de la vía");
    public static readonly AccidentType Rollover = new("Vuelco");
    public static readonly AccidentType Other = new("Otro");
}