using AccidentesDeMadrid.Entity;

namespace AccidentesDeMadrid;

public abstract class UtilLinq
{
    public static void Print(List<Accident> accidentGroup)
    {
        var count = 1;
        foreach (var acc in accidentGroup)
        {
            Console.WriteLine(
                $"{count}. - " +
                $"{acc.CaseNumber} - " +
                $"{acc.Date} - " +
                $"{acc.Time} - " +
                $"{acc.Locate} - " +
                $"{acc.StreetNumber} - " +
                $"{acc.District.Code} - " +
                $"{acc.District.Name} - " +
                $"{acc.AccidentType.message} - " +
                $"{acc.WeatherCondition?.Message} - " +
                $"{acc.VehicleType?.Message} - " +
                $"{acc.PersonRole.Message} - " +
                $"{acc.AgeRange} - " +
                $"{acc.Gender?.Message} - " +
                $"{acc.InjurySeverity?.Code} - " +
                $"{acc.InjurySeverity?.Message} - " +
                $"{acc.UtmXCoordinate} - " +
                $"{acc.UtmYCoordinate} - " +
                $"{acc.IsAlcoholPositive} - " +
                $"{acc.IsDrugPositive} "
            );
            count++;
        }
    }
}