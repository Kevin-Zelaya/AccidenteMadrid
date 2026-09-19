namespace AccidentesDeMadrid.Entity.Enum;

public record Gender(string Message)
{
    public static readonly Gender Male = new("Hombre");
    public static readonly Gender Female = new("Mujer");
    public static readonly Gender Unknown = new("Desconocido");
}