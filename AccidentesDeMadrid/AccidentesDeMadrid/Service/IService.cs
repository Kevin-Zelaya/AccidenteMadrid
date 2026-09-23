using AccidentesDeMadrid.Entity;

namespace AccidentesDeMadrid.Service;

public interface IService
{
    public Task LoadData();
    public List<Accident> GetAll();
    public void ClearData();
}