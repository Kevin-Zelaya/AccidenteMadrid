using System.Globalization;
using System.Reflection;
using AccidentesDeMadrid.Dto;
using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;
using CsvHelper;

namespace AccidentesDeMadrid.Mapper;

public static class AccidentMapper
{   
    public static Accident ToEntity(this AccidentDto dto)
    {
        return new Accident(
            dto.CaseNumber,
            ParseDateTime(dto.Date),
            TimeOnly.Parse(dto.Time),
            dto.Locate,
            dto.StreetNumber,
            new District(
                short.Parse(dto.DistrictCode),
                dto.DistrictName),
            ParseAccidentType(dto.AccidentType),
            ParseWeatherCondition(dto.WeatherCondition),
            ParseVehicleType(dto.VehicleType),
            dto.AgeRange,
            ParseGender(dto.gender),
            ParseInjurySeverity(dto.InjurySeverityCode),
            dto.UtmXCoordinate,
            dto.UtmYCoordinate,
            ParseAlcoholPositive(dto.IsAlcoholPositive),
            ParseDrugsPositive(dto.IsDrugPositive)
        );
    }

    // Parsear campos
    public static DateTime ParseDateTime(string dateTime)
    {
        return DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture); // parse  de la fecha para que reconozca el formato dia/mes/año
    }
    // Condición climatica
    public static WeatherCondition? ParseWeatherCondition(string weatherCondition)
    {
        return typeof(WeatherCondition) // Selecciona una clase, dandonos su tipo como un ejemplo.class
            .GetFields(BindingFlags.Public | BindingFlags.Static) // Se obtienen los campos que cumplan con lsos criterios de esr "Publico" o "estatico" y nos los retorna 
            .Select(f => f.GetValue(null) as WeatherCondition) // obtenemos el valor de esos campos #null es para estaticos y lo convierte a la clse especificada
            .FirstOrDefault(w => w.Message == weatherCondition);// filtramos, obtniendo el que cumpla con la cadena ue se le ha pasado
    }
    
    // tipo de accidente
    public static AccidentType ParseAccidentType(string type)
    {
        return typeof(AccidentType)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetValue(null) as AccidentType)
            .FirstOrDefault(a => a.message == type);
    }
    
    // Condición climatica
    public static Gender? ParseGender(string gender)
    {
        return typeof(Gender)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetValue(null) as Gender)
            .FirstOrDefault(g => g.Message == gender);
    }

    public static VehicleType? ParseVehicleType(string vehicleType)
    {
        return typeof(VehicleType)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetValue(null) as VehicleType)
            .FirstOrDefault(v => v.Message == vehicleType);
    }
    public static InjurySeverity? ParseInjurySeverity(string code)
    {
        if (!short.TryParse(code, out var iS))
            return null;
        return typeof(InjurySeverity)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetValue(null) as InjurySeverity)
            .FirstOrDefault(i => i.Code == iS);
    }

    public static bool ParseAlcoholPositive(string message)
    {
        return message.ToLower() switch
        {
            "s" => true,
            "n" => false,
            _ => false
        };
    }
    
    public static bool ParseDrugsPositive(string message)
    {
        return message switch
        {
            "1" => true,
            _ => false
        };
    }

}