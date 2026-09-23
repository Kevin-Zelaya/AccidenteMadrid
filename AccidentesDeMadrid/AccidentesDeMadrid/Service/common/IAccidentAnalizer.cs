using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;

namespace AccidentesDeMadrid.Service;

public interface IAccidentAnalizer
{
    int GetTotalAccidentsAsync();
    public Task LoadData();
    IDictionary<string, List<Accident>> GetAccidentsByDistrictAsync();

    public IDictionary<string, List<Accident>>  GetAccidentsByTypeAsync();

    public IDictionary<string, List<Accident>> GetAccidentsByWeatherAsync();

    public IDictionary<string, List<Accident>> GetAccidentsBySexAsync();

    public IDictionary<string, List<Accident>> GetAccidentsByAgeRangeAsync();

    public IDictionary<bool, List<Accident>>  GetAlcoholPositivesAsync();

    public IDictionary<bool, List<Accident>> GetDrugPositivesAsync();

    public IDictionary<DayOfWeek, List<Accident>> GetAccidentsByDayOfWeekAsync();

    public IDictionary<int, List<Accident>> GetAccidentsByMonthAsync();

    public (TimeOnly time, List<Accident> Accidents, int Total) GetPeakAccidentHourAsync();

    public (string Injury, List<Accident> Accidents, int Total) GetMostFrequentInjuriesAsync();

    public (string type, List<Accident> Accidents, int Total) GetMostInvolvedVehicleTypeAsync();

    (int Total, List<Accident> Accidents)  GetPedestrianAccidentsAsync();

    public (int Male, int Female, double MalePercentage, double FemalePercentage) GetMaleFemaleProportionAsync();
    
    public (string Name, int Total)[] GetDistrictsWithMostPedestriansAsync();

    public IEnumerable<(string Type, int Total)> GetWeekendVsWeekdayAsync();
    
    
    
    public IEnumerable<(DateTime Date, int Total)> GetAverageAccidentsPerDayAsync();
    

    
    IEnumerable<(string Range, int Total)> GetMostVulnerableAgeRangesAsync();

    

    int GetAccidentsWithAlcoholAndDrugsAsync();

    

    public IEnumerable<(string District, int Total)> GetDistrictsWithMostAlcoholPositivesAsync();

    public IEnumerable<(District District, List<Accident> Accidents)> GetAccidentsByDistrictCodeAsync();

    public IEnumerable<(int Year, int Total)> GetAccidentsByYearAsync();

    public IEnumerable<(int Year, int Month, int Total)> GetMonthlyEvolutionByYearAsync();

    public IEnumerable<(int Year, string Name, int Total)> GetDistrictWithMostAccidentsByYearAsync();

    public IEnumerable<(int Year, bool IsAlcoholPositive, int Total)> GetAlcoholTrendByYearAsync();

    public IEnumerable<(int Year, string Tag, int Total)> GetWeekendVsWeekdayByYearAsync();

    public IEnumerable<(int Year, int Time, int Total)> GetPeakHourByYearAsync();

    public IEnumerable<(int Year, string Injury, int Total)> GetMostFrequentInjuryByYearAsync();

    public IEnumerable<(int Year, int Total)> GetPedestrianTrendByYearAsync();
}