using Deedle;

namespace AccidentesDeMadrid.Repository;

public interface IDataframeRepository
{
    public Task LoadData();
    public Frame<int, string> GetALL();
    public void ClearData();
}