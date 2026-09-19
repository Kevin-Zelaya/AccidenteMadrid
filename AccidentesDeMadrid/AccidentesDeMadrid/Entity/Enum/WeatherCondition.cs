namespace AccidentesDeMadrid.Entity.Enum;

public record WeatherCondition(string Message)
{
    public static readonly WeatherCondition Clear = new("Despejado");
    public static readonly WeatherCondition Hailing = new("Granizando");
    public static readonly WeatherCondition LightRain = new("Lluvia débil");
    public static readonly WeatherCondition HeavyRain = new("Lluvia intensa");
    public static readonly WeatherCondition Snowing = new("Nevando");
    public static readonly WeatherCondition Cloudy = new("Nublado");
    public static readonly WeatherCondition Unknown = new("Se desconoce");
}
