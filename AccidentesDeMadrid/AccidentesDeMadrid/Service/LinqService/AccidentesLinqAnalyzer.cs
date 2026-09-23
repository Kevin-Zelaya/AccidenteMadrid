using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;
using AccidentesDeMadrid.Interfaces;
using AccidentesDeMadrid.Repository;

namespace AccidentesDeMadrid.Service;

public class AccidentesLinqAnalyzer : ILinqAccidentAnalizer, IScopedService
{
    private readonly IRepository _repository;
    public AccidentesLinqAnalyzer(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public async Task LoadData()
    {
        await _repository.LoadData();
    }
    /// <summary>Obtener resultado</summary>
    public int GetTotalAccidentsAsync() 
    {
        return _repository.GetALL()
            .Count();
        
    }
    /// <summary>Accidentes por distrito</summary>
    public IDictionary<string, List<Accident>> GetAccidentsByDistrictAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.District.Name) // se agrupa por el nombre del distrito
            .ToDictionary(
                g => g.Key, // Diccionario que contiene la clave (en este caso nombre del distrito)
                g => g.Take(3).ToList() //  y como valor la lista de accidentes
            );
    }
    /// <summary>Accidentes por tipo</summary>
    public IDictionary<string, List<Accident>> GetAccidentsByTypeAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.AccidentType) // Agrupamos por tio de accidente
            .ToDictionary(
                g => g.Key.message, // Tipo de accidente como clave
                g => g.Take(3).ToList() // Lista de accidentes de ese tipo
            );
    }
    /// <summary>Accidentes por ocndición climatica</summary>
    public IDictionary<string, List<Accident>> GetAccidentsByWeatherAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.WeatherCondition.Message) // Se agrupa por condición climatica
            .ToDictionary(
                g => g.Key, // Condición climatica como clave
                g => g.Take(3).ToList()); // Lista de accidentes 
    }
    /// <summary>Obtener accidentes por sexo</summary>
    public IDictionary<string, List<Accident>> GetAccidentsBySexAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.Gender.Message)
            .ToDictionary(
                g => g.Key, // Accidentes por genero Hombre/Mujer/Sin especificar
                g => g.Take(3).ToList()); // Lsta de accidente para cada genero
    }
    /// <summary>Obtenerlos por rango de edad</summary>
    public IDictionary<string, List<Accident>> GetAccidentsByAgeRangeAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.AgeRange) // Se agrupa por rango de edad
            .ToDictionary(
                g => g.Key, // Rango de edad como clave
                g => g.Take(3).ToList()); // Lista para cada rango
    }
    /// <summary>Positivos en alcohol</summary>
    public IDictionary<bool, List<Accident>> GetAlcoholPositivesAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.IsAlcoholPositive) // Por positivos o no en alcohol
            .ToDictionary(
                g => g.Key,
                g => g.Take(3).ToList());
    }
    /// <summary>Posititvos en drogas</summary>
    public IDictionary<bool, List<Accident>> GetDrugPositivesAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.IsDrugPositive) // Agrupar por positivos en drogas
            .ToDictionary(
                g => g.Key, // Booleano como clave
                g => g.Take(3).ToList()); // lista para cada opción
    }
    /// <summary>Por día de la semana</summary>
    public IDictionary<DayOfWeek, List<Accident>> GetAccidentsByDayOfWeekAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.Date.DayOfWeek) // Agrupar ppor día de semna
            .OrderBy(g => g.Key)
            .ToDictionary(
                g => g.Key, // día de la semana como clave
                g => g.Take(3).ToList()); // Lista de claves para cada caso
    }
    /// <summary>Por mes</summary>
    public IDictionary<int, List<Accident>> GetAccidentsByMonthAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.Date.Month) // Agrupar por mes 
            .OrderBy(g => g.Key) // Se ordena por mes
            .ToDictionary(
                g => g.Key, // mes como clave
                g => g.Take(3).ToList()); // Lista de accidentes por ese mes
    }
    /// <summary>Hora con más accidentes</summary>
    public (TimeOnly time, List<Accident> Accidents, int Total) GetPeakAccidentHourAsync() 
    {
        return _repository.GetALL()
            .GroupBy(r => r.Time) // Agrupar por hora
            .OrderByDescending(g => g.Count()) // Ordenar de mayor a menor
            .Select(g =>  ( // Tupla con...
                Time: g.Key, // Tiempo
                Accidents: g.Take(5).ToList(), // Lista de accidentes
                Total: g.Count() // Total
            ))
            .First(); // Obtengo el primero
    }
    /// <summary>Tipo de lesión más frecuente</summary>
    public (string Injury, List<Accident> Accidents, int Total) GetMostFrequentInjuriesAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.InjurySeverity.Message) // Se agrupa por lesión 
            .OrderByDescending(g => g.Count()) // Se ordena de mayor a menor
            .Select(g => (
                Injury: g.Key,
                Accidents: g.Take(5).ToList(),
                Total: g.Count()
            ))
            .First(); // Se obtiene el mayor
    }
    /// <summary>Vehículo más frecuente en accidentes</summary>
    public (string type, List<Accident> Accidents, int Total) GetMostInvolvedVehicleTypeAsync()
    {
        return _repository.GetALL()
            .GroupBy(r => r.VehicleType.Message) // Se agrupa por tipo de vehículo
            .OrderByDescending(g => g.Count()) // Se ordena de forma descendiente
            .Select(g => (
                Injury: g.Key,
                Accidents: g.Take(5).ToList(),
                Total: g.Count()
            ))
            .First(); // Obtenemos el primero
    }

    /// <summary>Proporción mujeres hombre</summary>
    public (int Male, int Female, double MalePercentage, double FemalePercentage) GetMaleFemaleProportionAsync()
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
    /// <summary>Obtener accidentes en los que este involucrado un peaton</summary>
    public (int Total, List<Accident> Accidents) GetPedestrianAccidentsAsync()
    {
        var accidents = _repository.GetALL()
            .Where(a => a.PersonRole == PersonType.Pedestrian) // Se filtra obteniendo solo los peatones
            .ToList();

        return (
            accidents.Count,
            accidents.Take(5).ToList()
        );
    }
    /// <summary>Distrito con más peatones</summary>
    public (string Name, int Total)[] GetDistrictsWithMostPedestriansAsync()
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
    /// <summary>Días de semana vs fines de semana</summary>
    public IEnumerable<(string Type, int Total)> GetWeekendVsWeekdayAsync()
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
    /// <summary>Promedio de accidentes por día</summary>
    public IEnumerable<(DateTime Date, int Total)> GetAverageAccidentsPerDayAsync()
    {
        return _repository.GetALL()
            .GroupBy(a => a.Date.Date)
            .Select(g => (
                Date: g.Key,
                Total: g.Count()
            ))
            .ToList();
    }
    /// <summary>Accidentes positivos en drogas y alcohol</summary>
    public int GetAccidentsWithAlcoholAndDrugsAsync()
    {
        return _repository.GetALL()
            .Count(a => a.IsAlcoholPositive == true && a.IsDrugPositive == true);
    }
    /// <summary>Rango de edad más vulverable</summary>
    public IEnumerable<(string Range, int Total)> GetMostVulnerableAgeRangesAsync()
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
    /// <summary>Distrito con más positivos en alcohol</summary>
    public IEnumerable<(string District, int Total)> GetDistrictsWithMostAlcoholPositivesAsync()
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
    /// <summary>Obtener por código del distrito</summary>
    public IEnumerable<(District District, List<Accident> Accidents)> GetAccidentsByDistrictCodeAsync()
    {
        return _repository.GetALL()
            .GroupBy(g => g.District)
            .Select(g => (
                distric: g.Key,
                list: g.Take(3).ToList()
            ))
            .ToList();
    }
    /// <summary>Accidentes por año</summary>
    public IEnumerable<(int Year, int Total)> GetAccidentsByYearAsync()
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
    /// <summary>Evolución mensual en los años</summary>
    public IEnumerable<(int Year, int Month, int Total)> GetMonthlyEvolutionByYearAsync()
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
    /// <summary>Distrito con más accidentes por año</summary>
    public IEnumerable<(int Year, string Name, int Total)> GetDistrictWithMostAccidentsByYearAsync()
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
    /// <summary>Evolución de accidentes con los años</summary>
    public IEnumerable<(int Year, bool IsAlcoholPositive, int Total)> GetAlcoholTrendByYearAsync()
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
    /// <summary>Tipo de lesión más frecuente</summary>
    public IEnumerable<(int Year, string Tag, int Total)> GetWeekendVsWeekdayByYearAsync()
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
    /// <summary>Hora pico de cada año</summary>
    public IEnumerable<(int Year, int Time, int Total)> GetPeakHourByYearAsync()
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
    /// <summary>Lesión más frecuente por año</summary>
    public IEnumerable<(int Year, string Injury, int Total)> GetMostFrequentInjuryByYearAsync()
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
    /// <summary>Evolución de los accidentes con peatones involucrados</summary>
    public IEnumerable<(int Year, int Total)> GetPedestrianTrendByYearAsync()
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