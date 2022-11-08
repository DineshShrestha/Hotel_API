using Hotel_HotelAPI.Models;
using System.Linq.Expressions;

namespace Hotel_HotelAPI.Repository.IRepository
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetAllAsync(Expression<Func<Hotel, bool>> filter = null);
        Task<Hotel> GetAsync(Expression<Func<Hotel, bool>> filter = null, bool tracked=true);

        Task UpdateAsync(Hotel entity);

        Task RemoveAsync(Hotel entity);

        Task CreateAsync(Hotel entity);

        Task SaveAsync();
    
    }
}
