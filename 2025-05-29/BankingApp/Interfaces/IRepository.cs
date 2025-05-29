using Microsoft.EntityFrameworkCore.Storage;

namespace BankingApp.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        Task<T> Add(T item);
        Task<T> Update(K id, T item);
        Task<T> Delete(K id);

        Task<ICollection<T>> GetAll();
        Task<T> Get(K id);

        Task<IDbContextTransaction> StartTransaction();
    }
}