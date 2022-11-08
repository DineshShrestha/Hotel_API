using Hotel_HotelAPI.Data;
using Hotel_HotelAPI.Models;
using Hotel_HotelAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel_HotelAPI.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly ApplicationDbContext _db;

        public HotelRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }
       
        public async Task<Hotel> UpdateAsync(Hotel entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Hotels.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
