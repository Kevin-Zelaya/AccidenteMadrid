using Microsoft.Data.Analysis;

namespace AccidentesDeMadrid.Repository;

public interface IDataframeRepository
{
    public Task LoadData();
    public DataFrame GetALL();
    public void ClearData();
}