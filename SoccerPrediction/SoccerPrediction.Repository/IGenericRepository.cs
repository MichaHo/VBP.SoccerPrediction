using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerPrediction.Repository
{
    public interface IGenericRepository<T>
    {
        bool Add(T item);
        Task<bool> AddAsync(T item);
        bool Update(T item);
        Task<bool> UpdateAsync(T item);
        bool Delete(T item);
        Task<bool> DeleteAsync(T item);
        T Get(string username, string pass);
        Task<T> GetAsync(string username, string pass);
        T Get(int id);
        Task<T> GetAsync(int id);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();

    }
}
