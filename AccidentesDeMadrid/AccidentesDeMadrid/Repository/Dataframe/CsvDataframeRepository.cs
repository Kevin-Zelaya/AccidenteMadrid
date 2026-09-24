using AccidentesDeMadrid.Interfaces;
using AccidentesDeMadrid.Repository.Load;
using Deedle;
using Microsoft.Data.Analysis;
using Microsoft.Extensions.Logging;

namespace AccidentesDeMadrid.Repository;

public class CsvDataframeRepository : IDataframeRepository, ISingletonService
{
    
    private readonly ILogger<CsvDataframeRepository> _logger; 
    private readonly Dictionary<string, string> _csvs; // Rutas de los csv guardados en un diccionario con <"Año", "Ruta">
    private readonly IDataframeLoader _dataframeLoader; // Cargador de csvs

    private Frame<int, string>? _data;
    
    public CsvDataframeRepository( // Inicializado automaticamente por scrutor
        ILogger<CsvDataframeRepository> logger,
        IDataframeLoader dataFrameLoader,
        Dictionary<string, string> csvs)
    {
        _logger = logger;
        _dataframeLoader = dataFrameLoader;
        _csvs = csvs;
    }
    
    public async Task LoadData()
    {
        _logger.LogInformation("[DATAFRAME-REPOSITORY] Intentando cargar datos desde el repositorio.");
        _data = await _dataframeLoader.LoadCsv(_csvs.Values); // Se pasan las rutas de los archivos
    }

    public Frame<int, string> GetALL()
    {
        return _data ?? throw new InvalidOperationException(
            "Los datos todavía no han sido cargados.");
    }

    public void ClearData()
    {
        _data = null;
    }
    
}