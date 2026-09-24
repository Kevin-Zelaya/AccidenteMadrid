using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using AccidentesDeMadrid;
using AccidentesDeMadrid.Configuration;
using AccidentesDeMadrid.Dependency;
using AccidentesDeMadrid.Entity.Enum;
using AccidentesDeMadrid.Repository;
using AccidentesDeMadrid.Repository.Load;
using AccidentesDeMadrid.Service;
using Microsoft.Extensions.DependencyInjection;

var provider = DependencyProvider.Configure();



Stopwatch stopwatch;
Console.WriteLine("===========================================================");
Console.WriteLine("=               Accidentes de madrid                      =");
Console.WriteLine("===========================================================");
var times = new long[2, 31]; //  tiempo total y para cada consulta el 31 es del proceso completo

// Implementación de IAccidentAnalizer, me permite trabajar con cualquier analizador
IAccidentAnalizer csvAnalizer;
//
var csvAnalizers = new(string name, IAccidentAnalizer service)[] // Array de  analizadores
{
    ("LINQ ANALIZER" ,provider.GetRequiredService<ILinqAccidentAnalizer>()),
    ("DATAFRAMES ANALIZER", provider.GetRequiredService<IDataframeAccidentAnalizer>())
}; // Implementan cada uno una interfaz diferentes pero que implemmentan IAccidentAnalizer

Stopwatch stopwatchAnalizer; // Contador para el total.
var anzInder = 0; // analizer index
var qrInder = 0; // query index


void RunQuery<T>( // Auxiliar para reutilizar declaraciones por cada consulta
    string title,
    Func<T> query,
    Action<T> print)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n[{qrInder+1}] {title}");
    Console.ResetColor();

    var stopwatch = Stopwatch.StartNew();

    var result = query();
    print(result);
    
    stopwatch.Stop();
    times[anzInder, qrInder] = stopwatch.ElapsedMilliseconds; // Se guarda el tiempo que tardó la consulta usando las dos variables indice
    
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"  {times[anzInder, qrInder]} ms");
    Console.ResetColor();
    qrInder++; // siguiente indice
}

string GetMonthName(int month) // Auxiliar para obtener el nombre de un mes
{ 
    return month switch
    {
        1 => "Enero",
        2 => "Febrero",
        3 => "Marzo",
        4 => "Abril",
        5 => "Mayo",
        6 => "Junio",
        7 => "Julio",
        8 => "Agosto",
        9 => "Septiembre",
        10 => "Octubre",
        11 => "Noviembre",
        12 => "Diciembre",
        _ => "Desconocido"
    };
}
foreach (var analizer in csvAnalizers) // Por cada analizer en el array, se ejecuta una vez
{
    qrInder = 0; // Indice de consultas a 0
    stopwatchAnalizer = Stopwatch.StartNew(); // Inicia contador para todo el analizer
    csvAnalizer = analizer.service; // Asignamos el analizer a la implementación
    await csvAnalizer.LoadData();
    
    Console.WriteLine("===========================================================");
    Console.WriteLine($"=                  {analizer.name}      =");
    Console.WriteLine("===========================================================");
    
    
    // 1
    RunQuery(
        "Total de accidentes",
        () => csvAnalizer.GetTotalAccidentsAsync(),
        result => Console.WriteLine($"Total: {result}")
        );
    // 2
    RunQuery(
        "Accidentes por distrito",
        () => csvAnalizer.GetAccidentsByDistrictAsync(),
        result =>
        {
            foreach (var r in result)
            {
                Console.WriteLine($"Distrito: {r.District} Total: {r.Total}");
            }
        });// 3
    RunQuery(
        "Accidentes por tipo",
        () => csvAnalizer.GetAccidentsByTypeAsync(),
        result =>
        {
            foreach (var r in result)
            {
                Console.WriteLine($"Tipo de accidente: {r.Type} Total: {r.Total}");
            }
        });
    // 4
    RunQuery(
        "Accidentes por estado meteorológico",
        () => csvAnalizer.GetAccidentsByWeatherAsync(),
        result =>
        {
            foreach (var r in result)
            {
                Console.WriteLine($"Estado meteorologico:: {r.Weather} Total: {r.Total}");
            }
        });
    // 5
    RunQuery(
        "Accidentes por sexo",
        () => csvAnalizer.GetAccidentsBySexAsync(),
        result =>
        {
            foreach (var r in result)
            {
                Console.WriteLine($"Genero: {r.Sex} Total: {r.Total}");
            }
        });
    // 6
    RunQuery(
        "Accidentes por edad",
        () => csvAnalizer.GetAccidentsByAgeRangeAsync(),
        result =>
        {
            foreach (var r in result)
            {
                Console.WriteLine($"Rango de edad: {r.Range} Total: {r.Total}");
            }
        });
    // 7
    RunQuery(
        "Positivos en alcohol",
        () => csvAnalizer.GetAlcoholPositivesAsync(),
        result =>
        {
            foreach (var r in result)
            {
                Console.WriteLine($"Alcohol: {(r.IsPositive?"Positivo": "Negativo")} Total: {r.Total}");
            }
        });
    // 8
    RunQuery(
        "Positivos en drogas",
        () => csvAnalizer.GetDrugPositivesAsync(),
        result =>
        {
            foreach (var r in result)
            {
                Console.WriteLine($"Drogas: {(r.IsPositive?"Positivo": "Negativo")} Total: {r.Total}");
            }
        });
    // 9
    RunQuery(
        "Accidentes por día de la semana",
        () => csvAnalizer.GetAccidentsByDayOfWeekAsync(),
        result =>
        {
            foreach (var r in result)
            {
                Console.WriteLine($"Dia:: {r.Day} Total: {r.Total}");
            }
        });
    // 10
    RunQuery(
        "Accidentes por mes",
        () => csvAnalizer.GetAccidentsByMonthAsync(),
        result =>
        {
            foreach (var r in result)
            {
                Console.WriteLine($"Mes: {GetMonthName(r.Month)} Total: {r.Total}");
            }
        });
    // 11
    RunQuery(
        "Hora con más accidentes",
        () => csvAnalizer.GetPeakAccidentHourAsync(),
        result => Console.WriteLine($"Hora: {result.Time} Total: {result.Total}")
        );
    // 12
    RunQuery(
        "Lesiones más frecuentes",
        () => csvAnalizer.GetMostFrequentInjuriesAsync(),
        result => Console.WriteLine($"Lesión: {result.Injury} Total: {result.Total}")
        );
    // 13
    RunQuery(
        "Tipo de vehículo más inplicado",
        () => csvAnalizer.GetMostInvolvedVehicleTypeAsync(),
        result => Console.WriteLine($"Lesión: {result.Type} Total: {result.Total}")
        );
    // 14
    RunQuery(
        "Accidentes con peatones",
        () => csvAnalizer.GetPedestrianAccidentsAsync(),
        result => Console.WriteLine($"Total: {result}")
        );
    // 15
    RunQuery(
        "Proporición homhre / mujer",
        () => csvAnalizer.GetMaleFemaleProportionAsync(),
        result =>
        {
            Console.WriteLine($"Hombres: {result.Male} {result.MalePercentage}%");
            Console.WriteLine($"Hombres: {result.Female} {result.FemalePercentage}%");
            
        });
    // 16
    RunQuery(
        "Distritos con más peatonesr",
        () => csvAnalizer.GetDistrictsWithMostPedestriansAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"Distito: {r.Name} Total: {r.Total}%");  
        });
    // 17
    RunQuery(
        "Fin de semana vs entre semana",
        () => csvAnalizer.GetWeekendVsWeekdayAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"{r.Type} Total: {r.Total}");  
            });
    // 18
    RunQuery(
        "Media de accidentes por día",
        () => csvAnalizer.GetAverageAccidentsPerDayAsync(),
        result => Console.WriteLine($"Promedio de accidentes: {result}")
            ); 
    // 19
    RunQuery(
        "Accidentes con alcohol + droga",
        () => csvAnalizer.GetAccidentsWithAlcoholAndDrugsAsync(),
        result => Console.WriteLine($"Total: {result}")
            ); 
    // 20
    RunQuery(
        "Rangos de edad más vulnerables",
        () => csvAnalizer.GetMostVulnerableAgeRangesAsync(),
        result => Console.WriteLine($"Rango de edad: {result}")
            ); 
    // 21
    RunQuery(
        "Distritos con más positivos en alcohol",
        () => csvAnalizer.GetDistrictsWithMostAlcoholPositivesAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"{r.District}: {r.Total}");
        }
    );

    // 22
    RunQuery(
        "Accidentes por código de distrito",
        () => csvAnalizer.GetAccidentsByDistrictCodeAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"{r.District}: {r.Total}");
        }
    );

    // 23
    RunQuery(
        "Accidentes por año",
        () => csvAnalizer.GetAccidentsByYearAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"   {r.Year}: {r.Total}");
        }
    );

    // 24
    RunQuery(
        "Evolución mensual por año",
        () => csvAnalizer.GetMonthlyEvolutionByYearAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"Yesa: {r.Year} Mes: {r.Month} Total: {r.Total}");
        }
    );

    // 25
    RunQuery(
        "Distrito con más accidentes por año",
        () => csvAnalizer.GetDistrictWithMostAccidentsByYearAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"Año: {r.Year} Distrito: {r.Name}  Total: ({r.Total})");
        }
    );

    // 26
    RunQuery(
        "Tendencia de alcohol por año",
        () => csvAnalizer.GetAlcoholTrendByYearAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"{r.Year} {(r.IsAlcoholPositive ? "Positivo" : "Negativo")}: {r.Total}");
        }
    );

    // 27
    RunQuery(
        "Comparativa fin de semana vs entre semana por año",
        () => csvAnalizer.GetWeekendVsWeekdayByYearAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"Año: {r.Year} {r.Tag} Total:{r.Total}");
        }
    );

    // 28
    RunQuery(
        "Hora pico por año",
        () => csvAnalizer.GetPeakHourByYearAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"Año: {r.Year} Hora: {r.Time}:00 Total: ({r.Total})");
        }
    );

    // 29
    RunQuery(
        "Lesión más frecuente por año",
        () => csvAnalizer.GetMostFrequentInjuryByYearAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($" Año: {r.Year} Lesion: {r.Injury} Total: ({r.Total})");
        }
    );

    // 30
    RunQuery(
        "Evolución de peatones por año",
        () => csvAnalizer.GetPedestrianTrendByYearAsync(),
        result =>
        {
            foreach (var r in result)
                Console.WriteLine($"   {r.Year}: {r.Total}");
        }
    );
    
    
    
    
    stopwatchAnalizer.Stop();
    times[anzInder, 30] = stopwatchAnalizer.ElapsedMilliseconds;
    anzInder++;
}

Console.WriteLine("\n--- Comparativa final ---");

for (var i = 0; i < csvAnalizers.Length; i++)
{
    var tiempos = Enumerable.Range(0, 30)
        .Select(x => times[i, x])
        .ToList();

    Console.WriteLine($"\n{csvAnalizers[i].name}");
    Console.WriteLine($"Tiempo total: {times[i, 30]} ms");
    Console.WriteLine($"Tiempo medio: {tiempos.Average():F2} ms");
    Console.WriteLine($"Más rápido: {tiempos.Min()} ms");
    Console.WriteLine($"Más lento: {tiempos.Max()} ms");
}
