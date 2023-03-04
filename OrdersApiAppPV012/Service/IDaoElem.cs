using OrdersApiAppPV012.Model.Entity;

namespace OrdersApiAppPV012.Service
{
    public interface IDaoElem<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T client);
        Task<T> Update(int id,T client);
        Task<bool> Delete(int id);
    }
}