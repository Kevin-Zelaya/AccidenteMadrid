using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Interfaces;
using AccidentesDeMadrid.Repository;
using Microsoft.Extensions.Logging;

namespace AccidentesDeMadrid.Service;

public class CSVAccidentService : IScopedService, IService
{
    private readonly ILogger<CSVAccidentRepository> _logger;
    private readonly IRepository _repository;

    public CSVAccidentService(
        ILogger<CSVAccidentRepository> logger,
        IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task LoadData()
    {
        await _repository.LoadData();
    }
    public List<Accident> GetAll()
    {
        return _repository.GetALL();
    }

    public void ClearData()
    {
        _repository.ClearData();
    }
}