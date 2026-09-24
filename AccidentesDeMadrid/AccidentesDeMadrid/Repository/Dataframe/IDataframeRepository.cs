using Deedle;
using Microsoft.Data.Analysis;

namespace AccidentesDeMadrid.Repository;

public interface IDataframeRepository
{
    public Task LoadData();
    public Frame<int, string> GetALL();
    public void ClearData();
}