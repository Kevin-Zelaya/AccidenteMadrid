using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;
using AccidentesDeMadrid.Interfaces;
using AccidentesDeMadrid.Repository;
using Microsoft.Data.Analysis;

namespace AccidentesDeMadrid.Service;

public class AccidentsDataframesAnalizer : IDataframeAccidentAnalizer, ITransientService
{
    private IDataframeRepository _repository;

    public AccidentsDataframesAnalizer(
        IDataframeRepository repository)
    {
        _repository = repository;
    }
    
    public async Task LoadData()
    {
        await _repository.LoadData();
    }
    /// <summary> Total de accidentes </summary>
    public IDictionary<string, List<Accident>> GetAccidentsByDistrictAsync()
    {
        throw new NotImplementedException();
    }

    
    public int GetTotalAccidentsAsync()
    {
        return (int)_repository.GetALL().Rows.Count;
    }

    /// <summary> Accidentes por distrito </summary>
    
    public IDictionary<string, List<Accident>> GetAccidentsByTypeAsync()
    {
        throw new NotImplementedException();
    }

    public IDictionary<string, List<Accident>> GetAccidentsByWeatherAsync()
    {
        throw new NotImplementedException();
    }

    public IDictionary<string, List<Accident>> GetAccidentsBySexAsync()
    {
        throw new NotImplementedException();
    }

    public IDictionary<string, List<Accident>> GetAccidentsByAgeRangeAsync()
    {
        throw new NotImplementedException();
    }

    public IDictionary<bool, List<Accident>> GetAlcoholPositivesAsync()
    {
        throw new NotImplementedException();
    }

    public IDictionary<bool, List<Accident>> GetDrugPositivesAsync()
    {
        throw new NotImplementedException();
    }

    public IDictionary<DayOfWeek, List<Accident>> GetAccidentsByDayOfWeekAsync()
    {
        throw new NotImplementedException();
    }

    public IDictionary<int, List<Accident>> GetAccidentsByMonthAsync()
    {
        throw new NotImplementedException();
    }

    public (TimeOnly time, List<Accident> Accidents, int Total) GetPeakAccidentHourAsync()
    {
        throw new NotImplementedException();
    }

    public (string Injury, List<Accident> Accidents, int Total) GetMostFrequentInjuriesAsync()
    {
        throw new NotImplementedException();
    }

    public (string type, List<Accident> Accidents, int Total) GetMostInvolvedVehicleTypeAsync()
    {
        throw new NotImplementedException();
    }

    public (int Total, List<Accident> Accidents) GetPedestrianAccidentsAsync()
    {
        throw new NotImplementedException();
    }

    public (int Male, int Female, double MalePercentage, double FemalePercentage) GetMaleFemaleProportionAsync()
    {
        throw new NotImplementedException();
    }

    public (string Name, int Total)[] GetDistrictsWithMostPedestriansAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(string Type, int Total)> GetWeekendVsWeekdayAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(DateTime Date, int Total)> GetAverageAccidentsPerDayAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(string Range, int Total)> GetMostVulnerableAgeRangesAsync()
    {
        throw new NotImplementedException();
    }

    public int GetAccidentsWithAlcoholAndDrugsAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(string District, int Total)> GetDistrictsWithMostAlcoholPositivesAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(District District, List<Accident> Accidents)> GetAccidentsByDistrictCodeAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(int Year, int Total)> GetAccidentsByYearAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(int Year, int Month, int Total)> GetMonthlyEvolutionByYearAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(int Year, string Name, int Total)> GetDistrictWithMostAccidentsByYearAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(int Year, bool IsAlcoholPositive, int Total)> GetAlcoholTrendByYearAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(int Year, string Tag, int Total)> GetWeekendVsWeekdayByYearAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(int Year, int Time, int Total)> GetPeakHourByYearAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(int Year, string Injury, int Total)> GetMostFrequentInjuryByYearAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(int Year, int Total)> GetPedestrianTrendByYearAsync()
    {
        throw new NotImplementedException();
    }
}