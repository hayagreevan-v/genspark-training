using BankingApp.Contexts;
using BankingApp.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankingApp.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly BankContext _bankContext;
        public Repository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }


        public async Task<T> Add(T item)
        {
            await _bankContext.AddAsync(item);
            await _bankContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(K id)
        {
            T? item = await Get(id);
            if (item == null)
                throw new Exception("No data found!");
            _bankContext.Remove(item);
            await _bankContext.SaveChangesAsync();
            return item;
        }

        public abstract Task<T> Get(K id);

        public abstract Task<ICollection<T>> GetAll();

        public async Task<T> Update(K id, T item)
        {
            T? myItem = await Get(id);
            _bankContext.Entry(myItem).CurrentValues.SetValues(item);
            await _bankContext.SaveChangesAsync();
            return item;
        }

        public async Task<IDbContextTransaction> StartTransaction()
        {
            return await _bankContext.Database.BeginTransactionAsync();
        }
    }
}