namespace AccidentesDeMadrid.Dto;

public class AccidentCsvRow
{
    public string NumExpediente { get; set; } = "";
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
    public string Localizacion { get; set; } = "";
    public string Numero { get; set; } = "";
    public string CodDistrito { get; set; } = "";
    public string Distrito { get; set; } = "";
    public string TipoAccidente { get; set; } = "";
    public string EstadoMeteorologico { get; set; } = "";
    public string TipoVehiculo { get; set; } = "";
    public string TipoPersona { get; set; } = "";
    public string RangoEdad { get; set; } = "";
    public string Sexo { get; set; } = "";
    public string CodLesividad { get; set; } = "";
    public string Lesividad { get; set; } = "";
    public string CoordenadaXUtm { get; set; } = "";
    public string CoordenadaYUtm { get; set; } = "";
    public string PositivaAlcohol { get; set; } = "";
    public string PositivaDroga { get; set; } = "";
}