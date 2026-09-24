using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;
using AccidentesDeMadrid.Interfaces;
using AccidentesDeMadrid.Repository;
using Deedle;

namespace AccidentesDeMadrid.Service;

/// <summary>
/// // Implementa IDataframeAccidentAnalizer que implementa IAccidentAnalizer y me ayuda a marcarla con una interfaz para scrutor
/// </summary>
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
    public int GetTotalAccidents()
    {
        //Task.Delay(350).Wait();
        return _repository
            .GetALL()
            .RowCount;
    }
    
    /// <summary> Accidentes por distritos </summary> *
    public IEnumerable<(string District, int Total)> GetAccidentsByDistrict()
    {
        var data = _repository.GetALL();
        
        
        var grouped = data.GroupRowsBy<string>("Distrito");

        return grouped
            .RowKeys
            .Select(g => (
                District: g.Item1,
                Total: g.Item2
            ))
            .OrderByDescending(x => x.Total)
            .Take(5)
            .ToList();
    }
    /// <summary> Accidentes por tipo </summary> *
    public IEnumerable<(string Type, int Total)> GetAccidentsByType()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var type = row.GetAs<string>("TipoAccidente");

            if (totals.ContainsKey(type))
                totals[type]++;
            else
                totals[type] = 1;
        }

        return totals
            .Select(x => (
                Type: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Accidentes por ocndición climatica</summary> *
    public IEnumerable<(string Weather, int Total)> GetAccidentsByWeather()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var weather = row.GetAs<string>("EstadoMeteorologico");

            if (totals.ContainsKey(weather))
                totals[weather]++;
            else
                totals[weather] = 1;
        }

        return totals
            .Select(x => (
                Weather: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Obtener accidentes por sexo</summary> *
    public IEnumerable<(string Sex, int Total)> GetAccidentsBySex()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var sex = row.GetAs<string>("Sexo");

            if (totals.ContainsKey(sex))
                totals[sex]++;
            else
                totals[sex] = 1;
        }

        return totals
            .Select(x => (
                Sex: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToList();

    }
    /// <summary>Obtenerlos por rango de edad</summary> *
    public IEnumerable<(string Range, int Total)> GetAccidentsByAgeRange()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var range = row.GetAs<string>("RangoEdad");

            if (totals.ContainsKey(range))
                totals[range]++;
            else
                totals[range] = 1;
        }

        return totals
            .Select(x => (
                Range: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Positivos en alcohol</summary> *
    public IEnumerable<(bool IsPositive, int Total)> GetAlcoholPositives()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<bool, int>();

        foreach (var row in data.Rows.Values)
        {
            var isPositive = row.GetAs<string>("PositivaAlcohol") == "S";

            if (totals.ContainsKey(isPositive))
                totals[isPositive]++;
            else
                totals[isPositive] = 1;
        }

        return totals
            .Select(x => (
                IsPositive: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Posititvos en drogas</summary> *
    public IEnumerable<(bool IsPositive, int Total)> GetDrugPositives()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<bool, int>();

        foreach (var row in data.Rows.Values)
        {
            var isPositive = row.GetAs<string>("PositivaDroga") == "1";

            if (totals.ContainsKey(isPositive))
                totals[isPositive]++;
            else
                totals[isPositive] = 1;
        }

        return totals
            .Select(x => (
                IsPositive: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Por día de la semana</summary> *
    public IEnumerable<(DayOfWeek Day, int Total)> GetAccidentsByDayOfWeek()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<DayOfWeek, int>();

        foreach (var row in data.Rows.Values)
        {
            var day = row.GetAs<DateTime>("Fecha").DayOfWeek;

            if (totals.ContainsKey(day))
                totals[day]++;
            else
                totals[day] = 1;
        }

        return totals
            .Select(x => (
                Day: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Por mes</summary> *
    public IEnumerable<(int Month, int Total)> GetAccidentsByMonth()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<int, int>();

        foreach (var row in data.Rows.Values)
        {
            var month = row.GetAs<DateTime>("Fecha").Month;

            if (totals.ContainsKey(month))
                totals[month]++;
            else
                totals[month] = 1;
        }

        return totals
            .Select(x => (
                Month: x.Key,
                Total: x.Value
            ))
            .OrderBy(x => x.Month)
            .ToList();
    }
    /// <summary>Hora con más accidentes</summary> *
    public (TimeOnly Time, int Total) GetPeakAccidentHour()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<int, int>();

        foreach (var row in data.Rows.Values)
        {
            var hour = row.GetAs<TimeSpan>("Hora").Hours;

            if (totals.ContainsKey(hour))
                totals[hour]++;
            else
                totals[hour] = 1;
        }

        var peak = totals
            .OrderByDescending(x => x.Value)
            .First();

        return (
            Time: new TimeOnly(peak.Key, 0),
            Total: peak.Value
        );
    }
    /// <summary>Tipo de lesión más frecuente</summary> *
    public (string Injury, int Total) GetMostFrequentInjuries()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var injury = row.GetAs<string>("Lesividad");

            if (totals.ContainsKey(injury))
                totals[injury]++;
            else
                totals[injury] = 1;
        }

        var result = totals
            .OrderByDescending(x => x.Value)
            .First();

        return (
            Injury: result.Key,
            Total: result.Value
        );
    }
    /// <summary>Vehículo más frecuente en accidentes</summary> *
    public (string Type, int Total) GetMostInvolvedVehicleType()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var type = row.GetAs<string>("TipoVehiculo");

            if (totals.ContainsKey(type))
                totals[type]++;
            else
                totals[type] = 1;
        }

        var result = totals
            .OrderByDescending(x => x.Value)
            .First();

        return (
            Type: result.Key,
            Total: result.Value
        );
    }
    /// <summary>Obtener accidentes en los que este involucrado un peaton</summary> *
    public int GetPedestrianAccidents()
    {
        var data = _repository.GetALL();

        var total = 0;

        foreach (var row in data.Rows.Values)
        {
            var personType = row.GetAs<string>("TipoPersona");

            if (personType == "Peatón")
                total++;
        }

        return total;
    }
    /// <summary>Proporción mujeres hombre</summary> *
    public (int Male, int Female, double MalePercentage, double FemalePercentage) GetMaleFemaleProportion()
    {
        var data = _repository.GetALL();

        var male = 0;
        var female = 0;

        foreach (var row in data.Rows.Values)
        {
            var sex = row.GetAs<string>("Sexo");

            if (sex == "Hombre")
                male++;
            else if (sex == "Mujer")
                female++;
        }

        var total = male + female;

        return (
            Male: male,
            Female: female,
            MalePercentage: (double)male / total * 100,
            FemalePercentage: (double)female / total * 100
        );
    }
    /// <summary>Distrito con más peatones</summary> *
    public (string Name, int Total)[] GetDistrictsWithMostPedestrians()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var personType = row.GetAs<string>("TipoPersona");

            if (personType != "Peatón")
                continue;

            var district = row.GetAs<string>("Distrito");

            if (totals.ContainsKey(district))
                totals[district]++;
            else
                totals[district] = 1;
        }

        return totals
            .Select(x => (
                Name: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToArray();
    }
    /// <summary>Días de semana vs fines de semana</summary> *
    public IEnumerable<(string Type, int Total)> GetWeekendVsWeekday()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var date = row.GetAs<DateTime>("Fecha");

            var type = date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday
                ? "Fin de semana"
                : "Entre semana";

            if (totals.ContainsKey(type))
                totals[type]++;
            else
                totals[type] = 1;
        }

        return totals
            .Select(x => (
                Type: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Promedio de accidentes por día</summary> *
    public double GetAverageAccidentsPerDay()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<DateTime, int>();

        foreach (var row in data.Rows.Values)
        {
            var date = row.GetAs<DateTime>("Fecha").Date;

            if (totals.ContainsKey(date))
                totals[date]++;
            else
                totals[date] = 1;
        }

        return totals.Values.Average();
    }
    /// <summary>Rango de edad más vulverable</summary> *
    public IEnumerable<(string Range, int Total)> GetMostVulnerableAgeRanges()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var range = row.GetAs<string>("RangoEdad");

            if (totals.ContainsKey(range))
                totals[range]++;
            else
                totals[range] = 1;
        }

        return totals
            .Select(x => (
                Range: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .Take(3)
            .ToList();
    }
    /// <summary>Accidentes positivos en drogas y alcohol</summary> *
    public int GetAccidentsWithAlcoholAndDrugs()
    {
        var data = _repository.GetALL();

        var total = 0;

        foreach (var row in data.Rows.Values)
        {
            var alcoholPositive = row.GetAs<string>("PositivaAlcohol") == "1";
            var drugsPositive = row.GetAs<string>("PositivaDroga") == "1";

            if (alcoholPositive && drugsPositive)
                total++;
        }

        return total;
    }
    /// <summary>Distrito con más positivos en alcohol</summary> *
    public IEnumerable<(string District, int Total)> GetDistrictsWithMostAlcoholPositives()
    {
        var data = _repository.GetALL();

        var result = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var alcohol = row.GetAs<string>("PositivaAlcohol");

            if (alcohol != "1")
                continue;

            var district = row.GetAs<string>("Distrito");

            if (result.ContainsKey(district))
                result[district]++;
            else
                result[district] = 1;
        }

        return result
            .OrderByDescending(x => x.Value)
            .Take(3)
            .Select(x => (
                District: x.Key,
                Total: x.Value
            ))
            .ToList();
    }
    /// <summary>Obtener por código del distrito</summary> *
    public IEnumerable<(string District, int Total)> GetAccidentsByDistrictCode()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<string, int>();

        foreach (var row in data.Rows.Values)
        {
            var district = row.GetAs<string>("CodDistrito");

            if (totals.ContainsKey(district))
                totals[district]++;
            else
                totals[district] = 1;
        }

        return totals
            .Select(x => (
                District: x.Key,
                Total: x.Value
            ))
            .OrderByDescending(x => x.Total)
            .ToList();
    }
    /// <summary>Accidentes por año</summary> *
    public IEnumerable<(int Year, int Total)> GetAccidentsByYear()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<int, int>();

        foreach (var row in data.Rows.Values)
        {
            var year = row.GetAs<DateTime>("Fecha").Year;

            if (totals.ContainsKey(year))
                totals[year]++;
            else
                totals[year] = 1;
        }

        return totals
            .Select(x => (
                Year: x.Key,
                Total: x.Value
            ))
            .OrderBy(x => x.Year)
            .ToList();
    }
    /// <summary>Evolución mensual en los años</summary> *
    public IEnumerable<(int Year, int Month, int Total)> GetMonthlyEvolutionByYear()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<(int Year, int Month), int>();

        foreach (var row in data.Rows.Values)
        {
            var date = row.GetAs<DateTime>("Fecha");

            var key = (date.Year, date.Month);

            if (totals.ContainsKey(key))
                totals[key]++;
            else
                totals[key] = 1;
        }

        return totals
            .Select(x => (
                Year: x.Key.Year,
                Month: x.Key.Month,
                Total: x.Value
            ))
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToList();
    }
    /// <summary>Distrito con más accidentes por año</summary> *
    public IEnumerable<(int Year, string Name, int Total)> GetDistrictWithMostAccidentsByYear()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<(int Year, string District), int>();

        foreach (var row in data.Rows.Values)
        {
            var date = row.GetAs<DateTime>("Fecha");
            var district = row.GetAs<string>("Distrito");

            var key = (date.Year, district);

            if (totals.ContainsKey(key))
                totals[key]++;
            else
                totals[key] = 1;
        }

        return totals
            .GroupBy(x => x.Key.Year)
            .Select(g =>
            {
                var max = g
                    .OrderByDescending(x => x.Value)
                    .First();

                return (
                    Year: g.Key,
                    Name: max.Key.District,
                    Total: max.Value
                );
            })
            .OrderBy(x => x.Year)
            .ToList();
    }
    /// <summary>Evolución de accidentes con los años</summary> *
    public IEnumerable<(int Year, bool IsAlcoholPositive, int Total)> GetAlcoholTrendByYear()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<(int Year, bool IsAlcoholPositive), int>();

        foreach (var row in data.Rows.Values)
        {
            var year = row.GetAs<DateTime>("Fecha").Year;
            var alcohol = row.GetAs<string>("PositivaAlcohol") == "1";

            var key = (year, alcohol);

            if (totals.ContainsKey(key))
                totals[key]++;
            else
                totals[key] = 1;
        }

        return totals
            .Select(x => (
                Year: x.Key.Year,
                IsAlcoholPositive: x.Key.IsAlcoholPositive,
                Total: x.Value
            ))
            .OrderBy(x => x.Year)
            .ThenBy(x => x.IsAlcoholPositive)
            .ToList();
    }
    /// <summary>Tipo de lesión más frecuente</summary> *
    public IEnumerable<(int Year, string Tag, int Total)> GetWeekendVsWeekdayByYear()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<(int Year, string Tag), int>();

        foreach (var row in data.Rows.Values)
        {
            var date = row.GetAs<DateTime>("Fecha");

            var tag = date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday
                ? "Fin de semana"
                : "Entre semana";

            var key = (date.Year, tag);

            if (totals.ContainsKey(key))
                totals[key]++;
            else
                totals[key] = 1;
        }

        return totals
            .Select(x => (
                Year: x.Key.Year,
                Tag: x.Key.Tag,
                Total: x.Value
            ))
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Tag)
            .ToList();
    }
    /// <summary>Hora pico de cada año</summary> *
    public IEnumerable<(int Year, int Time, int Total)> GetPeakHourByYear()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<(int Year, int Time), int>();

        foreach (var row in data.Rows.Values)
        {
            var year = row.GetAs<DateTime>("Fecha").Year;
            var time = row.GetAs<TimeSpan>("Hora").Hours;

            var key = (year, time);

            if (totals.ContainsKey(key))
                totals[key]++;
            else
                totals[key] = 1;
        }

        return totals
            .GroupBy(x => x.Key.Year)
            .Select(g =>
            {
                var peak = g.OrderByDescending(x => x.Value).First();

                return (
                    Year: g.Key,
                    Time: peak.Key.Time,
                    Total: peak.Value
                );
            })
            .OrderBy(x => x.Year)
            .ToList();
    }
    /// <summary>Lesión más frecuente por año</summary> *
    public IEnumerable<(int Year, string Injury, int Total)> GetMostFrequentInjuryByYear()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<(int Year, string Injury), int>();

        foreach (var row in data.Rows.Values)
        {
            var year = row.GetAs<DateTime>("Fecha").Year;
            var injury = row.GetAs<string>("Lesividad");

            if (string.IsNullOrWhiteSpace(injury))
                continue;

            var key = (year, injury);

            if (totals.ContainsKey(key))
                totals[key]++;
            else
                totals[key] = 1;
        }

        return totals
            .GroupBy(x => x.Key.Year)
            .Select(g =>
            {
                var mostFrequent = g
                    .OrderByDescending(x => x.Value)
                    .First();

                return (
                    Year: g.Key,
                    Injury: mostFrequent.Key.Injury,
                    Total: mostFrequent.Value
                );
            })
            .OrderBy(x => x.Year)
            .ToList();
    }

    public IEnumerable<(int Year, int Total)> GetPedestrianTrendByYear()
    {
        var data = _repository.GetALL();

        var totals = new Dictionary<int, int>();

        foreach (var row in data.Rows.Values)
        {
            var year = row.GetAs<DateTime>("Fecha").Year;
            var personType = row.GetAs<string>("TipoPersona");

            if (personType != "Peatón")
                continue;

            if (totals.ContainsKey(year))
                totals[year]++;
            else
                totals[year] = 1;
        }

        return totals
            .Select(x => (
                Year: x.Key,
                Total: x.Value
            ))
            .OrderBy(x => x.Year)
            .ToList();
    }
}