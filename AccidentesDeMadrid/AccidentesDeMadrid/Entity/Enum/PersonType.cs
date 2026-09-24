namespace AccidentesDeMadrid.Entity.Enum;

public record PersonType(string Message)
{
    public static readonly PersonType Driver = new("Conductor");
    public static readonly PersonType Passenger = new("Pasajero");
    public static readonly PersonType Pedestrian = new("Peatón");
}
