namespace AccidentesDeMadrid.Dto;
/// <summary>
/// Se utiliza al cargar los csv para darle
/// un formato temporal a los registros obtenidos
/// para mapear posteriormente
/// </summary>
/// <param name="CaseNumber"></param>
/// <param name="Date"></param>
/// <param name="Time"></param>
/// <param name="Locate"></param>
/// <param name="StreetNumber"></param>
/// <param name="DistrictCode"></param>
/// <param name="DistrictName"></param>
/// <param name="Type"></param>
/// <param name="WeatherCondition"></param>
/// <param name="VehicleType"></param>
/// <param name="AgeRange"></param>
/// <param name="gender"></param>
/// <param name="InjurySeverity"></param>
/// <param name="UtmXCoordinate"></param>
/// <param name="UtmYCoordinate"></param>
/// <param name="IsAlcoholPositive"></param>
/// <param name="IsDrugPositive"></param>
public record AccidentDto(
    string CaseNumber,
    string Date,
    string Time,
    string Locate,
    string StreetNumber,
    string DistrictCode,
    string DistrictName,
    string AccidentType,
    string WeatherCondition,
    string VehicleType,
    string PersonRole,
    string AgeRange,
    string gender,
    string InjurySeverityCode,
    string InjurySeverity,
    string UtmXCoordinate,
    string UtmYCoordinate,
    string IsAlcoholPositive, 
    string IsDrugPositive
    );