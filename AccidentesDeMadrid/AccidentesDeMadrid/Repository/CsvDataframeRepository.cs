using AccidentesDeMadrid.Interfaces;
using AccidentesDeMadrid.Repository.Load;
using Microsoft.Data.Analysis;
using Microsoft.Extensions.Logging;

namespace AccidentesDeMadrid.Repository;

public class CsvDataframeRepository : IDataframeRepository, ITransientService
{
    private readonly ILogger<CsvDataframeRepository> _logger;
    private readonly IDataframeLoader _dataFrameLoader;
    private readonly Dictionary<string, string> _csvs;

    private DataFrame _data = new();

    public CsvDataframeRepository(
        ILogger<CsvDataframeRepository> logger,
        IDataframeLoader dataFrameLoader,
        Dictionary<string, string> csvs)
    {
        _logger = logger;
        _dataFrameLoader = dataFrameLoader;
        _csvs = csvs;
    }

    public async Task LoadData()
    {
        _data = await _dataFrameLoader.LoadCsv(_csvs.Values);
    }

    public DataFrame GetALL()
    {
        return _data;
    }

    public void ClearData()
    {
        _data = new DataFrame();
    }
}