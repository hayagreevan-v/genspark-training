using FirstAPI.Models;

namespace FirstAPI.Interfaces
{
    public interface IService<T>
    {
        IEnumerable<T> Search(SearchModel searchModel);
        T Add(T item);
    }
}