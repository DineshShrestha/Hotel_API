using Hotel_HotelAPI.Models;
using System.Linq.Expressions;

namespace Hotel_HotelAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);

        

        Task RemoveAsync(T entity);

        Task CreateAsync(T entity);

        Task SaveAsync();
    }
}
