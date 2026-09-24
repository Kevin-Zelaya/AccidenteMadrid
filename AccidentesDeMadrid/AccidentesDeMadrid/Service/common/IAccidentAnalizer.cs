using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;

namespace AccidentesDeMadrid.Service;

public interface IAccidentAnalizer
{
    int GetTotalAccidents();
    public Task LoadData();
    public IEnumerable<(string District, int Total)> GetAccidentsByDistrict();

    public IEnumerable<(string Type, int Total)> GetAccidentsByType();

    public IEnumerable<(string Weather, int Total)> GetAccidentsByWeather();

    public IEnumerable<(string Sex, int Total)> GetAccidentsBySex();

    public IEnumerable<(string Range, int Total)> GetAccidentsByAgeRange();

    public IEnumerable<(bool IsPositive, int Total)> GetAlcoholPositives();

    public IEnumerable<(bool IsPositive, int Total)> GetDrugPositives();

    public IEnumerable<(DayOfWeek Day, int Total)> GetAccidentsByDayOfWeek();

    public IEnumerable<(int Month, int Total)> GetAccidentsByMonth();

    public (TimeOnly Time, int Total) GetPeakAccidentHour();

    public (string Injury, int Total) GetMostFrequentInjuries(); //

    public (string Type, int Total) GetMostInvolvedVehicleType();//

    int GetPedestrianAccidents(); //

    public (int Male, int Female, double MalePercentage, double FemalePercentage) GetMaleFemaleProportion();
    
    public (string Name, int Total)[] GetDistrictsWithMostPedestrians();

    public IEnumerable<(string Type, int Total)> GetWeekendVsWeekday();
    
    public double GetAverageAccidentsPerDay();
    
    IEnumerable<(string Range, int Total)> GetMostVulnerableAgeRanges();

    int GetAccidentsWithAlcoholAndDrugs();

    public IEnumerable<(string District, int Total)> GetDistrictsWithMostAlcoholPositives();

    public IEnumerable<(string District, int Total)>  GetAccidentsByDistrictCode();

    public IEnumerable<(int Year, int Total)> GetAccidentsByYear();

    public IEnumerable<(int Year, int Month, int Total)> GetMonthlyEvolutionByYear();

    public IEnumerable<(int Year, string Name, int Total)> GetDistrictWithMostAccidentsByYear();

    public IEnumerable<(int Year, bool IsAlcoholPositive, int Total)> GetAlcoholTrendByYear();

    public IEnumerable<(int Year, string Tag, int Total)> GetWeekendVsWeekdayByYear();

    public IEnumerable<(int Year, int Time, int Total)> GetPeakHourByYear();

    public IEnumerable<(int Year, string Injury, int Total)> GetMostFrequentInjuryByYear();

    public IEnumerable<(int Year, int Total)> GetPedestrianTrendByYear();
}