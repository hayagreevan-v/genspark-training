using System;

namespace Questions_PatientManagementSystem.Interfaces;

public interface IRepository<K, T> where T : class
{
    T Add(T patient);
    T GetById(K id);
    ICollection<T> GetAll();
}
