using Hotel_HotelAPI.Data;
using Hotel_HotelAPI.Models;
using Hotel_HotelAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel_HotelAPI.Repository
{
    public class HotelNumberRepository : Repository<HotelNumber>, IHotelNumberRepository
    {
        private readonly ApplicationDbContext _db;

        public HotelNumberRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }
       
        public async Task<HotelNumber> UpdateAsync(HotelNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.HotelNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
