using Hotel_HotelAPI.Models;
using System.Linq.Expressions;

namespace Hotel_HotelAPI.Repository.IRepository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<Hotel> UpdateAsync(Hotel entity);

    }
}
