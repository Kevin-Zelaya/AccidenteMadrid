using AccidentesDeMadrid.Entity;

namespace AccidentesDeMadrid.Repository.Load;

public interface ICsvLoader
{
    public Task<IEnumerable<Accident>> LoadCsv(string path);
}