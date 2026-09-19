using AccidentesDeMadrid.Dependency;
using AccidentesDeMadrid.Repository.Load;
using Microsoft.Extensions.DependencyInjection;

var provider = DependencyProvider.Configure();

var transiend = provider.CreateScope();

var loader = transiend.ServiceProvider.GetRequiredService<ICsvLoader>();
var directoryPath = Path.Combine("Data", "accidentes_trafico_2024.csv");
var registros = await loader.LoadCsv( Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryPath));

foreach (var VARIABLE in registros)
{
    Console.WriteLine(VARIABLE.GetType()+" probar: "+ VARIABLE.gender);
}
