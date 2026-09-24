using AccidentesDeMadrid.Entity;

namespace AccidentesDeMadrid.Repository;

public interface IRepository
{
    public Task LoadData();
    public List<Accident> GetALL();
    public List<Accident> GetByYear(string year);
    public void ClearData();
}