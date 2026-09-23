using AccidentesDeMadrid.Entity.Enum;

namespace AccidentesDeMadrid.Entity;

public record Accident(
        string CaseNumber,
        DateTime Date,
        TimeOnly Time,
        string Locate,
        string? StreetNumber,
        District District,
        AccidentType AccidentType,
        WeatherCondition? WeatherCondition,
        VehicleType? VehicleType,
        PersonType PersonRole,
        string AgeRange,
        Gender Gender,
        InjurySeverity? InjurySeverity,
        string? UtmXCoordinate,
        string? UtmYCoordinate,
        bool IsAlcoholPositive,
        bool IsDrugPositive
        // Nos quedamos por aqui
    );