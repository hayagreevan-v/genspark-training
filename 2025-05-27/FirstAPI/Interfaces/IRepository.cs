namespace FirstAPI.Interfaces
{
    public interface IRepository<K, T>
    {
        IEnumerable<T> GetAll();

        T Add(T item);
        T? Get(K id);
        K GenerateID();
    }
}