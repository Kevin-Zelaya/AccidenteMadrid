using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using AccidentesDeMadrid;
using AccidentesDeMadrid.Configuration;
using AccidentesDeMadrid.Dependency;
using AccidentesDeMadrid.Entity.Enum;
using AccidentesDeMadrid.Repository;
using AccidentesDeMadrid.Repository.Load;
using AccidentesDeMadrid.Service;
using Microsoft.Extensions.DependencyInjection;

var provider = DependencyProvider.Configure();

var transiend = provider.CreateScope();

var loader = transiend.ServiceProvider.GetRequiredService<ICsvLoader>();
var directoryPath = Path.Combine("Data", "accidentes_trafico_2024.csv");
IService service = provider.GetRequiredService<IService>();
ILinqAccidentAnalizer analizerLinq = provider.GetRequiredService<ILinqAccidentAnalizer>();

Stopwatch stopwatch;
/*
Console.WriteLine("===========================================================");
Console.WriteLine("=               Accidentes de madrid                      =");
Console.WriteLine("===========================================================");

// Cargar data
await analizerLinq.LoadData();

Console.WriteLine("=================================================================");
Console.WriteLine("                1. NÚMERO TOTAL DE ACCIDENTES");
Console.WriteLine("=================================================================");
// Para medir el tiempo de ejecución del proceso
stopwatch = Stopwatch.StartNew();
var result = analizerLinq.GetTotalAccidentsAsync();
Console.WriteLine($"1. Accidentes registrados en los últimos tres años: {result}");
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();


Console.WriteLine("=================================================================");
Console.WriteLine("                2. ACCIDENTES POR DISTRITO (TOP 5)");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();

var data2 = analizerLinq.GetAccidentsByDistrictAsync();

foreach (var distric in data2)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  {distric.Key}  ===");
    Console.ResetColor();
    var count = 1;
    UtilLinq.Print(distric.Value);
}
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();

Console.WriteLine("=================================================================");
Console.WriteLine("                    3. ACCIDENTES POR TIPO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();
var accidentsType = analizerLinq.GetAccidentsByTypeAsync();

foreach (var accidentType in accidentsType)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  {accidentType.Key}  ===");
    Console.ResetColor();
    
    UtilLinq.Print(accidentType.Value);
}
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();

Console.WriteLine("=================================================================");
Console.WriteLine("               4. ACCIDENTES POR CONDICIÓN CLIMATICA");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();

var weatherCondition = analizerLinq.GetAccidentsByWeatherAsync();
    
    
foreach (var weather in weatherCondition)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  {weather.Key}  ===");
    Console.ResetColor();
    
    UtilLinq.Print(weather.Value);
}
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================");
Console.WriteLine("                   5. ACCIDENTES POR SEXO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();
var genderCausse = analizerLinq.GetAccidentsBySexAsync();
    
foreach (var gender in genderCausse)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  {gender.Key}  ===");
    Console.ResetColor();
    
    UtilLinq.Print(gender.Value);
}
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================");
Console.WriteLine("                   6. ACCIDENTES POR RANGO DE EDAD");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();
var ageRange = analizerLinq.GetAccidentsByAgeRangeAsync();
foreach (var age in ageRange)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  {age.Key}  ===");
    Console.ResetColor();
    
    UtilLinq.Print(age.Value);
}
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================");
Console.WriteLine("                  7. POSITIVOS EN ALCOHOL");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();
var alcoholPositive = analizerLinq.GetAlcoholPositivesAsync();
foreach (var alcohol in alcoholPositive)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  {(alcohol.Key?"Positivos":"Negativos")}  ===");
    Console.ResetColor();
    
    UtilLinq.Print(alcohol.Value);
}
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================");
Console.WriteLine("                  8. POSITIVOS EN DROGAS");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();
var drugsPositive = analizerLinq.GetDrugPositivesAsync();
    
foreach (var drugs in drugsPositive)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  {(drugs.Key?"Positivos":"Negativos")}  ===");
    Console.ResetColor();
    
    UtilLinq.Print(drugs.Value);
}
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================");
Console.WriteLine("                  9. DÍA DE LA SEMANA");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();
var DayOfWeek = analizerLinq.GetAccidentsByDayOfWeekAsync();
foreach (var day in DayOfWeek)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  {day.Key}  ===");
    Console.ResetColor();
    
    UtilLinq.Print(day.Value);
}
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
//service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                  10. POR MES DEL AÑO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var PerMonth = analizerLinq.GetAccidentsByMonthAsync();
    
foreach (var month in PerMonth)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  {GetMonthName(month.Key)}  ===");
    Console.ResetColor();
    
    UtilLinq.Print(month.Value);
}

string GetMonthName(int number)
{
    return number switch
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
    };
}
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
//service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                  11. HORA CON MÁS ACCIDENTES");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var HourWithMostAccidents = analizerLinq.GetPeakAccidentHourAsync();
    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  ===  Más accidentes a las: {HourWithMostAccidents.time}  ==="); 
    Console.WriteLine($"  ===  Total: {HourWithMostAccidents.Total}  ==="); 
    Console.ResetColor();
    
    UtilLinq.Print(HourWithMostAccidents.Accidents);

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                 12. LESIONES MÁS FRECUENTES");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var injurySeverity = analizerLinq.GetMostFrequentInjuriesAsync();
    
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"  ===  Se presentan con más lesiones del tipo: {injurySeverity.Injury}  ==="); 
Console.WriteLine($"  ===  Total: {injurySeverity.Total}  ==="); 
Console.ResetColor();
    
UtilLinq.Print(injurySeverity.Accidents);

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
//service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                 13. VEHÍCULO MÁS IMPLICADO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var vehicleInMostAccident = analizerLinq.GetMostInvolvedVehicleTypeAsync();
    
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"  ===  El vehículo más frecuente en los accidentes: {vehicleInMostAccident.type}  ==="); 
Console.WriteLine($"  ===  Total: {vehicleInMostAccident.Total}  ==="); 
Console.ResetColor();
    
UtilLinq.Print(injurySeverity.Accidents);

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
//service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                  14. ACCIDENTES CON PEATONES");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var PersonRole = analizerLinq.GetPedestrianAccidentsAsync();
    
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"  ===  Accidentes con peatones: {PersonRole.Total}  ==="); 
Console.ResetColor();
    
UtilLinq.Print(PersonRole.Accidents);

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
//service.ClearData();


Console.WriteLine("=================================================================");
Console.WriteLine("                 15. PROPORCIÓN HOMBRE/MUJER");
Console.WriteLine("=================================================================");

stopwatch = Stopwatch.StartNew();

var proportion = analizerLinq.GetMaleFemaleProportionAsync();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("  ===  Proporción hombre/mujer  ===");
Console.WriteLine($"    Hombres: {proportion.Male} ({proportion.MalePercentage:F2}%)");
Console.WriteLine($"    Mujeres: {proportion.Female} ({proportion.FemalePercentage:F2}%)");
Console.ResetColor();

stopwatch.Stop();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();

Console.WriteLine("=================================================================\n\n");

service.ClearData();


Console.WriteLine("=================================================================");
Console.WriteLine("                 16. DISTRITOS CON MÁS PEATONES");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var DistricWithMorePedestrians = analizerLinq.GetDistrictsWithMostPedestriansAsync();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"  ===  Distritos con más peatones. (Top 3)  ===");
var count3 = 1;
foreach (var d in DistricWithMorePedestrians)
{
    Console.WriteLine($"{count3}. {d.Name}: {d.Total}");
}

Console.ResetColor();

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
//service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                 17. DÍA DE SEMANA VS FIN DE SEMANA");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var daysVsWeekend = analizerLinq.GetWeekendVsWeekdayAsync();

var total = daysVsWeekend.Sum(x => x.Total);

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("  ===  Días de semana vs fines de semana  ===");

foreach (var d in daysVsWeekend)
{
    Console.WriteLine(
        $"{d.Type}. {d.Total}: {(double)d.Total / total * 100:F2}%"
    );
}


Console.ResetColor();

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
//service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                 18. MEDIA DE ACCIDENTES POR DÍA");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();

var average = analizerLinq.GetAverageAccidentsPerDayAsync();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"  ===  Media de accidentes por día  ===");
Console.WriteLine($"    {average:F2} accidentes por día");
Console.ResetColor();
stopwatch.Stop();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();

Console.WriteLine("=================================================================\n\n");

service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                 19. ACCIDENTES CON ALCOHOL + DROGA");
Console.WriteLine("=================================================================");

stopwatch = Stopwatch.StartNew();

var accidentsAlcoholAndDrugs =
    analizerLinq.GetAccidentsWithAlcoholAndDrugsAsync();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("  ===  Accidentes con alcohol + droga  ===");
Console.WriteLine($"    Total: {accidentsAlcoholAndDrugs} accidentes");
Console.ResetColor();

stopwatch.Stop();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();

Console.WriteLine("=================================================================\n\n");

service.ClearData();


Console.WriteLine("=================================================================");
Console.WriteLine("                 20. RANGOS DE EDAD MÁS VULNERABLES");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var vulnerableAgeGroups = analizerLinq.GetMostVulnerableAgeRangesAsync();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("  ===  Rangos de edad más vulnerables  ===");

foreach (var g in vulnerableAgeGroups)
{
    Console.WriteLine($"    {g.Range}: {g.Total} accidentes totales.");
}


Console.ResetColor();
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();


Console.WriteLine("=================================================================");
Console.WriteLine("                 21. DISTRITOS CON MÁS POSITIVOS EN ALCOHOL");
Console.WriteLine("=================================================================");

stopwatch = Stopwatch.StartNew();
var districtsAlcohol =
    analizerLinq.GetDistrictsWithMostAlcoholPositivesAsync();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("  ===  Distritos con más positivos en alcohol  ===");

foreach (var district in districtsAlcohol)
{
    Console.WriteLine($"    {district.District}: {district.Total} positivos");
}

Console.ResetColor();

stopwatch.Stop();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();

Console.WriteLine("=================================================================\n\n");

service.ClearData();


Console.WriteLine("=================================================================");
Console.WriteLine("              22. ACCIDENTES POR CÓDIGO DE DISTRITO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var PerDistricId = analizerLinq.GetAccidentsByDistrictCodeAsync();
    

foreach (var d in PerDistricId)
{
    Console.WriteLine($"  === {d.District.Name}: {d.District.Code} ===");
    UtilLinq.Print(d.Accidents);
}
Console.ResetColor();
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                  23.  ACCIDENTES POR AÑO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var accidentsPerYear = analizerLinq.GetAccidentsByYearAsync();
    
Console.ForegroundColor = ConsoleColor.Yellow;

Console.WriteLine("   ===  Accidentes por año  ====");
foreach (var year in accidentsPerYear)
{
    Console.WriteLine($"Año {year.Year}: {year.Total}");
}
Console.ResetColor();
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                 24. EVOLUCIÓN MENSUAL CON LOS AÑOS");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var anualChange = analizerLinq.GetMonthlyEvolutionByYearAsync();

var lastMonth = 0;
foreach (var month in anualChange)
{
    string porcentajeTexto = lastMonth > 0 
        ? (((double)(month.Total - lastMonth) / lastMonth) * 100).ToString("f2") + "%" 
        : "N/A"; 

    Console.WriteLine($"Mes: {month.Month}/{month.Year} - Total: {month.Total} - Porcentaje: {porcentajeTexto}");
    
    lastMonth = month.Total;
}


Console.ForegroundColor = ConsoleColor.Yellow;
Console.ResetColor();
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("             25. DISTRITO CON MÁS ACCIDENTES POR AÑO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var DistrictMoreAccidentPerYear = analizerLinq.GetDistrictWithMostAccidentsByYearAsync();
Console.ForegroundColor = ConsoleColor.Yellow;
foreach (var district in DistrictMoreAccidentPerYear)
{
    Console.WriteLine($"Año: {district.Year} Distrito: {district.Name} Total: {district.Total}");
}

Console.ResetColor();
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("               26. TENDENCIA DE ALCOHOL POR AÑO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var AlcoholChange = analizerLinq.GetAlcoholTrendByYearAsync();

var lastYear = 0;
foreach (var year in AlcoholChange)
{
    string porcentajeTexto = lastYear > 0 
        ? (((double)(year.Total - lastYear) / lastYear) * 100).ToString("f2") + "%" 
        : "N/A"; 

    Console.WriteLine($"Año: {year.Year} - Total: {year.Total} - Porcentaje: {porcentajeTexto}");
    
    lastYear = year.Total;
}


Console.ForegroundColor = ConsoleColor.Yellow;
Console.ResetColor();
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("            27. DÍA DE SEMANA VS FIN DE SEMANA POR AÑO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var dayVsEndPerYear = analizerLinq.GetWeekendVsWeekdayByYearAsync();

var totalPerYear = dayVsEndPerYear
    .GroupBy(g => g.Year)
    .ToDictionary(g => g.Key, g => g.Sum(x => x.Total));
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"  ===  Días de semana vs fines de semana por año  ===");
foreach (var d in dayVsEndPerYear)
{
    int totalYear = totalPerYear[d.Year];
    double porcentaje = ((double)d.Total / totalYear) * 100;

    Console.WriteLine($"{d.Year} - {d.Total}: {d.Total} ({porcentaje:f2}%)");
}

Console.ResetColor();

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                  28. HORA PICO POR AÑO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var HourPerYear = analizerLinq.GetPeakHourByYearAsync();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"  ===  Horas picos  ===");
foreach (var h in HourPerYear)
{
    Console.WriteLine($"{h.Year}: hora {h.Time}, accidentes {h.Total}");
}

Console.ResetColor();

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                 29. LESION MÁS FRECUENTE CADA AÑO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var injurySeverityPerYear = analizerLinq.GetMostFrequentInjuryByYearAsync();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"  ===  Lesión más frecuente por año  ===");
foreach (var i in injurySeverityPerYear)
{
    Console.WriteLine($"{i.Year}: tipo {i.Injury}, accidentes {i.Total}");
}

Console.ResetColor();

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
service.ClearData();

Console.WriteLine("=================================================================");
Console.WriteLine("                30. TENDENCIA DE PEATONES POR AÑO");
Console.WriteLine("=================================================================");
stopwatch = Stopwatch.StartNew();


var PedestrianTrendByYear = analizerLinq.GetPedestrianTrendByYearAsync();

var lastYear2 = 0;
foreach (var y in PedestrianTrendByYear)
{
    var percentage = lastYear2 > 0
        ? ((double)(y.Total - lastYear2) / lastYear2) * 100
        : 0;

    Console.WriteLine($"{y.Year}: Total {y.Total} Evolución {percentage:F2}%");
    lastYear2 = y.Total;
}
Console.ResetColor();

stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Tiempo de ejecución en ms: {stopwatch.ElapsedMilliseconds}");
Console.ResetColor();
Console.WriteLine("=================================================================\n\n");
*/
///
/// Final LINQ
///

IDataframeAccidentAnalizer dataframeAnalizer = provider.GetRequiredService<IDataframeAccidentAnalizer>();
Console.WriteLine("=================================================================");
Console.WriteLine("                 1. TOTAL DE ACCIDENTES");
Console.WriteLine("=================================================================");

await dataframeAnalizer.LoadData();

stopwatch = Stopwatch.StartNew();

var total = dataframeAnalizer.GetTotalAccidentsAsync();

Console.WriteLine($"Total de accidentes: {total}");

stopwatch.Stop();
Console.WriteLine($"Tiempo: {stopwatch.ElapsedMilliseconds} ms");
