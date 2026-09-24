using Deedle;
using Microsoft.Data.Analysis;

namespace AccidentesDeMadrid.Repository.Load;

public interface IDataframeLoader
{
    Task<Frame<int, string>> LoadCsv(IEnumerable<string> paths);
    
}