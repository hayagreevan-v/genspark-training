using ConsoleApp1.Interfaces;
using ConsoleApp1.Exceptions;

namespace ConsoleApp1.Repositories
{
    abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        
        public abstract ICollection<T> GetAll();
        public abstract T GetById(K id);
        protected abstract K GenerateID();

        protected List<T> _items = new List<T>();

        public T Add(T item)
        {
            var id = GenerateID();
            var property = typeof(T).GetProperty("Id");
            if (property != null)
            {
                property.SetValue(item, id);
            }
            if (_items.Contains(item))
            {
                throw new DuplicateEntityException("Already Exists");
            }
            _items.Add(item);

            return item;
        }

        public T Delete(K id)
        {
            var item = GetById(id);
            if (item == null)
                throw new KeyNotFoundException("Item not found");
            _items.Remove(item);
            return item;
        }

        public T Update(T item)
        {
            var listItem = GetById((K)item!.GetType().GetProperty("Id")!.GetValue(item!)!);
            // var listItem = _items.FirstOrDefault(i => i.Equals(item));
            if (listItem == null)
            {
                throw new KeyNotFoundException("No item found");
            }
            var index = _items.IndexOf(listItem);
            _items[index] = item;
            return item;
        }
    }
}