using Hotel_HotelAPI.Models;
using System.Linq.Expressions;

namespace Hotel_HotelAPI.Repository.IRepository
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetAll(Expression<Func<Hotel>> filter = null);
        Task<Hotel> Get(Expression<Func<Hotel>> filter = null, bool tracked=true);

        Task<Hotel> Update(Hotel entity);

        Task<Hotel> Remove(Hotel entity);

        Task Create(Hotel entity);

        Task Save();
    }
}
