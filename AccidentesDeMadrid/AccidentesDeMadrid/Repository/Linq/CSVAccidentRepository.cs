using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Interfaces;
using AccidentesDeMadrid.Repository.Load;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AccidentesDeMadrid.Repository;

public class CSVAccidentRepository : IRepository, ITransientService
{
    private readonly ILogger<CSVAccidentRepository> _logger;
    private readonly ICsvLoader _csvLoader;
    private readonly Dictionary<string, string> _csvs;
    private readonly Dictionary<string, List<Accident>> _data = new(); // Tres diccionario, cada uno con el el respectivo año como llave 
    public CSVAccidentRepository(
        ILogger<CSVAccidentRepository> logger,
        ICsvLoader csvLoader,
        Dictionary<string, string> csvs
        )
    {
        _csvLoader = csvLoader;
        _logger = logger;
        _csvs = csvs;
    }

    public async Task LoadData()
    {

        var tasks = _csvs.Select(async file =>
        {
            //_logger.LogInformation("[CSV-REPOSITORY] Obteniendo archivo csv del año: {year}",file.Key);
            var accidents = await _csvLoader.LoadCsv(file.Value);

            return new
            {
                Year = file.Key,
                Accidents = accidents.ToList()
            };

        });
        var result = await Task.WhenAll(tasks);

        foreach (var d in result)
        {
            _data.Add(d.Year, d.Accidents);
        }
        
    }
    public List<Accident> GetALL()
    {
        return _data.SelectMany(list => list.Value).ToList();
    }

    public List<Accident> GetByYear(string year)
    {
        return _data.FirstOrDefault(d => d.Key == year).Value;
    }

    public void ClearData()
    {
        _data.Clear();
    }
}