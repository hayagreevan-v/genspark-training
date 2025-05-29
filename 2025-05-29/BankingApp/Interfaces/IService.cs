using System;

namespace BankingApp.Interfaces;

public interface IService<K,T,Tdto>
{
    Task<T> Add(Tdto item);
    Task<T> View(K id);
    Task<ICollection<T>> ViewAll();
}
