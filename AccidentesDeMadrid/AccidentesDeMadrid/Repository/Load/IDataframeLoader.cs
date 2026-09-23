using Microsoft.Data.Analysis;

namespace AccidentesDeMadrid.Repository.Load;

public interface IDataframeLoader
{
    Task<DataFrame> LoadCsv(IEnumerable<string> paths);
    
}