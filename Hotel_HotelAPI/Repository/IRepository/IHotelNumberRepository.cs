using Hotel_HotelAPI.Models;
using System.Linq.Expressions;

namespace Hotel_HotelAPI.Repository.IRepository
{
    public interface IHotelNumberRepository : IRepository<HotelNumber>
    {
        Task<HotelNumber> UpdateAsync(HotelNumber entity);

    }
}
