using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;

namespace AccidentesDeMadrid.Service;

public interface IAccidentAnalizer
{
    int GetTotalAccidentsAsync();
    public Task LoadData();
    public IEnumerable<(string District, int Total)> GetAccidentsByDistrictAsync();

    public IEnumerable<(string Type, int Total)> GetAccidentsByTypeAsync();

    public IEnumerable<(string Weather, int Total)> GetAccidentsByWeatherAsync();

    public IEnumerable<(string Sex, int Total)> GetAccidentsBySexAsync();

    public IEnumerable<(string Range, int Total)> GetAccidentsByAgeRangeAsync();

    public IEnumerable<(bool IsPositive, int Total)> GetAlcoholPositivesAsync();

    public IEnumerable<(bool IsPositive, int Total)> GetDrugPositivesAsync();

    public IEnumerable<(DayOfWeek Day, int Total)> GetAccidentsByDayOfWeekAsync();

    public IEnumerable<(int Month, int Total)> GetAccidentsByMonthAsync();

    public (TimeOnly Time, int Total) GetPeakAccidentHourAsync();

    public (string Injury, int Total) GetMostFrequentInjuriesAsync(); //

    public (string Type, int Total) GetMostInvolvedVehicleTypeAsync();//

    int GetPedestrianAccidentsAsync(); //

    public (int Male, int Female, double MalePercentage, double FemalePercentage) GetMaleFemaleProportionAsync();
    
    public (string Name, int Total)[] GetDistrictsWithMostPedestriansAsync();

    public IEnumerable<(string Type, int Total)> GetWeekendVsWeekdayAsync();
    
    
    
    public double GetAverageAccidentsPerDayAsync();
    

    
    IEnumerable<(string Range, int Total)> GetMostVulnerableAgeRangesAsync();

    

    int GetAccidentsWithAlcoholAndDrugsAsync();

    

    public IEnumerable<(string District, int Total)> GetDistrictsWithMostAlcoholPositivesAsync();

    public IEnumerable<(string District, int Total)> GetAccidentsByDistrictCodeAsync();

    public IEnumerable<(int Year, int Total)> GetAccidentsByYearAsync();

    public IEnumerable<(int Year, int Month, int Total)> GetMonthlyEvolutionByYearAsync();

    public IEnumerable<(int Year, string Name, int Total)> GetDistrictWithMostAccidentsByYearAsync();

    public IEnumerable<(int Year, bool IsAlcoholPositive, int Total)> GetAlcoholTrendByYearAsync();

    public IEnumerable<(int Year, string Tag, int Total)> GetWeekendVsWeekdayByYearAsync();

    public IEnumerable<(int Year, int Time, int Total)> GetPeakHourByYearAsync();

    public IEnumerable<(int Year, string Injury, int Total)> GetMostFrequentInjuryByYearAsync();

    public IEnumerable<(int Year, int Total)> GetPedestrianTrendByYearAsync();
}