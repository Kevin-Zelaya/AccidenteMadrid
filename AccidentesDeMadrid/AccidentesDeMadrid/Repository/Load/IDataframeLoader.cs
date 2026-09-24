using Deedle;

namespace AccidentesDeMadrid.Repository.Load;

public interface IDataframeLoader
{
    Task<Frame<int, string>> LoadCsv(IEnumerable<string> paths);
    
}