using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;
using AccidentesDeMadrid.Interfaces;
using AccidentesDeMadrid.Repository;
using Microsoft.Extensions.Logging;

namespace AccidentesDeMadrid.Service;

public class AccidentesLinqAnalyzer : ILinqAccidentAnalizer, IScopedService
{
    private readonly IRepository _repository;
    private readonly ILogger<IAccidentAnalizer> _logger;
    public AccidentesLinqAnalyzer(
        IRepository repository,
        ILogger<IAccidentAnalizer> logger
        )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task LoadData()
    {
        _logger.LogInformation("[LINQ-ANALIZER] Intentando cargar datos desde el analizer.");
        await _repository.LoadData();
    }
    /// <summary>Obtener resultado</summary>
    public int GetTotalAccidents() 
    {
        return _repository.GetALL()
            .Count();
        
    }
    /// <summary>Accidentes por distrito</summary> *
    public IEnumerable<(string District, int Total)> GetAccidentsByDistrict()
    {
        return _repository.GetALL()
            .GroupBy(r => r.District.Name) // se agrupa por el nombre del distrito
            .Select(g => (
                District: g.Key, 
                Total: g.Count()
            ));
    }
    /// <summary>Accidentes por tipo</summary> *
    public IEnumerable<(string Type, int Total)> GetAccidentsByType()
    {
        return _repository.GetALL()
            .GroupBy(r => r.AccidentType.message) // Agrupamos por tio de accidente
            .Select(g => (
                Type: g.Key,
                Total: g.Count()
            ));
    }
    /// <summary>Accidentes por ocndición climatica</summary> *
    public IEnumerable<(string Weather, int Total)> GetAccidentsByWeather()
    {
        return _repository.GetALL()
            .GroupBy(r => r.WeatherCondition.Message) // Se agrupa por condición climatica
            .Select(g => (
                Weather: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Obtener accidentes por sexo</summary> *
    public IEnumerable<(string Sex, int Total)> GetAccidentsBySex()
    {
        return _repository.GetALL()
            .GroupBy(r => r.Gender.Message)
            .Select(g => (
                Sex: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Obtenerlos por rango de edad</summary> *
    public IEnumerable<(string Range, int Total)> GetAccidentsByAgeRange()
    {
        return _repository.GetALL()
            .GroupBy(r => r.AgeRange) // Se agrupa por rango de edad
            .Select(g => (
                Range: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Positivos en alcohol</summary> *
    public IEnumerable<(bool IsPositive, int Total)> GetAlcoholPositives()
    {
        return _repository.GetALL()
            .GroupBy(r => r.IsAlcoholPositive) // Por positivos o no en alcohol
            .Select(g => (
                IsPositive: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Posititvos en drogas</summary> *
    public IEnumerable<(bool IsPositive, int Total)> GetDrugPositives()
    {
        return _repository.GetALL()
            .GroupBy(r => r.IsDrugPositive) // Agrupar por positivos en drogas
            .Select(g => (
                IsPositive: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Por día de la semana</summary> *
    public IEnumerable<(DayOfWeek Day, int Total)> GetAccidentsByDayOfWeek()
    {
        return _repository.GetALL()
            .GroupBy(r => r.Date.DayOfWeek) // Agrupar ppor día de semna
            .Select(g => (
                Day: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Por mes</summary> *
    public IEnumerable<(int Month, int Total)> GetAccidentsByMonth()
    {
        return _repository.GetALL()
            .GroupBy(r => r.Date.Month) // Agrupar por mes 
            .Select(g => (
                Month: g.Key,
                Total: g.Count()
            ))
            .OrderBy(x => x.Month)
            .ToList(); // Lista de accidentes por ese mes
    }
    /// <summary>Hora con más accidentes</summary> *
    public (TimeOnly Time, int Total) GetPeakAccidentHour()
    {
        return _repository.GetALL()
            .GroupBy(r => r.Time.Hour)
            .Select(g => (
                Time: new TimeOnly(g.Key, 0),
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .First();
    }
    /// <summary>Tipo de lesión más frecuente</summary> *
    public (string Injury, int Total) GetMostFrequentInjuries()
    {
        return _repository.GetALL()
            .GroupBy(r => r.InjurySeverity.Message) // Se agrupa por lesión 
            .Select(g => (
                Injury: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .First();
    }
    /// <summary>Vehículo más frecuente en accidentes</summary> *
    public (string Type, int Total) GetMostInvolvedVehicleType()
    {
        return _repository.GetALL()
            .GroupBy(r => r.VehicleType.Message) // Se agrupa por tipo de vehículo
            .Select(g => (
                Type: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .First();

    }

    /// <summary>Obtener accidentes en los que este involucrado un peaton</summary> *
    public int GetPedestrianAccidents()
    {
        return _repository.GetALL()
            .Count(a => a.PersonRole == PersonType.Pedestrian);

    }
    /// <summary>Proporción mujeres hombre</summary> *
    public (int Male, int Female, double MalePercentage, double FemalePercentage) GetMaleFemaleProportion()
    {
        var accidents = _repository.GetALL();
        var male = accidents.Count(a => a.Gender == Gender.Male); // Obtenemos total de hombres
        var female = accidents.Count(a => a.Gender == Gender.Female); // Obtenemo total de mujeres
        var total = male + female; // Suma de ambos
        return (
            Male: male,
            Female: female,
            MalePercentage: (double)male / total * 100, // Porcentaje de cada uno
            FemalePercentage: (double)female / total * 100
        );
    }
    
    /// <summary>Distrito con más peatones</summary> *
    public (string Name, int Total)[] GetDistrictsWithMostPedestrians()
    {
        return _repository.GetALL()
            .Where(a => a.PersonRole == PersonType.Pedestrian)
            .GroupBy(g => g.District)
            .Select(g => 
            (
                Name: g.Key.Name,
                Total: g.Count()
            ))
            .OrderByDescending(g => g.Total)
            .Take(3)
            .ToArray();
    }
    /// <summary>Días de semana vs fines de semana</summary> *
    public IEnumerable<(string Type, int Total)> GetWeekendVsWeekday()
    {
        return _repository.GetALL()
            .GroupBy(a =>
                a.Date.DayOfWeek == System.DayOfWeek.Saturday ||
                a.Date.DayOfWeek == System.DayOfWeek.Sunday
                    ? "Fin de semana"
                    : "Día de semana")
            .Select(g => (
                Type: g.Key,
                Total: g.Count()
            ))
            .ToList();
    }
    /// <summary>Promedio de accidentes por día</summary> *
    public double GetAverageAccidentsPerDay()
    {
        return _repository.GetALL()
            .GroupBy(a => a.Date.Date)
            .Select(g => g.Count())
            .Average();
    }
    /// <summary>Rango de edad más vulverable</summary> *
    public IEnumerable<(string Range, int Total)> GetMostVulnerableAgeRanges()
    {
        return _repository.GetALL()
            .GroupBy(a => a.AgeRange)
            .Select(g => (
                Range: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(g => g.Total)
            .Take(3)
            .ToList();
    }
    /// <summary>Accidentes positivos en drogas y alcohol</summary> *
    public int GetAccidentsWithAlcoholAndDrugs()
    {
        return _repository.GetALL()
            .Count(a => a.IsAlcoholPositive == true && a.IsDrugPositive == true);
    }
    
    /// <summary>Distrito con más positivos en alcohol</summary> *
    public IEnumerable<(string District, int Total)> GetDistrictsWithMostAlcoholPositives()
    {
        return _repository.GetALL()
            .Where(a => a.IsAlcoholPositive == true)
            .GroupBy(a => a.District.Name)
            .Select(g => (
                District: g.Key,
                Total: g.Count()
            ))
            .OrderByDescending(g => g.Total)
            .Take(3)
            .ToList();
    }
    /// <summary>Obtener por código del distrito</summary> *
    public IEnumerable<(string District, int Total)> GetAccidentsByDistrictCode()
    {
        return _repository.GetALL()
            .GroupBy(g => g.District)
            .Select(g => (
                District: g.Key.ToString(),
                Total: g.Count()
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Accidentes por año</summary> *
    public IEnumerable<(int Year, int Total)> GetAccidentsByYear()
    {
        return _repository.GetALL()
            .GroupBy(g => g.Date.Year)
            .Select(g => (
                Year: g.Key,
                Total: g.Count()
            ))
            .OrderBy(g => g.Year)
            .ToList();
    }
    /// <summary>Evolución mensual en los años</summary> *
    public IEnumerable<(int Year, int Month, int Total)> GetMonthlyEvolutionByYear()
    {
        return _repository.GetALL()
            .GroupBy(g => new { g.Date.Month, g.Date.Year })
            .Select(g => (
                Year: g.Key.Year,
                Month: g.Key.Month,
                Total: g.Count()
            ))
            .OrderBy(g => g.Year)
            .ThenBy(g => g.Month)
            .ToList();
    }
    /// <summary>Distrito con más accidentes por año</summary> *
    public IEnumerable<(int Year, string Name, int Total)> GetDistrictWithMostAccidentsByYear()
    {
        return _repository.GetALL()
            .GroupBy(g => new { g.Date.Year, g.District.Name })
            .Select(g => (
                Year: g.Key.Year,
                Name: g.Key.Name,
                Total: g.Count()
            ))
            .OrderBy(g => g.Year)
            .ThenBy(g => g.Name)
            .ToList();
    }
    /// <summary>Evolución de accidentes con los años</summary> *
    public IEnumerable<(int Year, bool IsAlcoholPositive, int Total)> GetAlcoholTrendByYear()
    {
        return _repository.GetALL()
            .Where(a => a.IsAlcoholPositive)
            .GroupBy(g => new { g.Date.Year, g.IsAlcoholPositive })
            .Select(g => (
                Year: g.Key.Year,
                IsAlcoholPositive: g.Key.IsAlcoholPositive,
                Total: g.Count()
            ))
            .OrderBy(g => g.Year)
            .ThenBy(g => g.IsAlcoholPositive)
            .ToList();
    }
    /// <summary>Tipo de lesión más frecuente</summary> *
    public IEnumerable<(int Year, string Tag, int Total)> GetWeekendVsWeekdayByYear()
    {
        return _repository.GetALL()
            .GroupBy(g => new
            {
                g.Date.Year,
                tag = (g.Date.DayOfWeek == System.DayOfWeek.Sunday || g.Date.DayOfWeek == System.DayOfWeek.Monday
                    ? "Fin de semana"
                    : "Día de semana")
            })
            .Select(g => (
                Year: g.Key.Year,
                Tag: g.Key.tag,
                Total: g.Count()
            ))
            .OrderBy(g => g.Year)
            .ThenBy(g => g.Tag)
            .ToList();
    }
    /// <summary>Hora pico de cada año</summary> *
    public IEnumerable<(int Year, int Time, int Total)> GetPeakHourByYear()
    {
        return _repository.GetALL()
            .GroupBy(g => g.Date.Year)
            .Select(g => g
                .GroupBy(a => a.Time.Hour)
                .Select(h => (
                    Year: g.Key,
                    Time: h.Key,
                    Total: h.Count()
                ))
                .OrderByDescending(g => g.Time)
                .First())
            .OrderByDescending(g => g.Year)
            .ToList();
    }
    /// <summary>Lesión más frecuente por año</summary> *
    public IEnumerable<(int Year, string Injury, int Total)> GetMostFrequentInjuryByYear()
    {
        return _repository.GetALL()
            .GroupBy(g => g.Date.Year)
            .Select(g => g
                .GroupBy(a => a.InjurySeverity)
                .Select(h => (
                    Year: g.Key,
                    Injury: h.Key.Message,
                    Total: h.Count()
                ))
                .OrderByDescending(g => g.Year)
                .First())
            .OrderByDescending(g => g.Year)
            .ToList();
    }
    /// <summary>Evolución de los accidentes con peatones involucrados</summary> *
    public IEnumerable<(int Year, int Total)> GetPedestrianTrendByYear()
    {
        return _repository.GetALL()
            .Where(a => a.PersonRole == PersonType.Pedestrian)
            .GroupBy(g => g.Date.Year)
            .Select(g => (
                Year: g.Key,
                Total: g.Count()
            ))
            .OrderBy(g => g.Year)
            .ToList();
    }
}