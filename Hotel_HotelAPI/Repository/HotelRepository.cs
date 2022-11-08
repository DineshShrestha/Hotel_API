using Hotel_HotelAPI.Data;
using Hotel_HotelAPI.Models;
using Hotel_HotelAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel_HotelAPI.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _db;

        public HotelRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Hotel entity)
        {
           await _db.Hotels.AddAsync(entity);
           await SaveAsync();
        }

        public async Task<Hotel> GetAsync(Expression<Func<Hotel, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Hotel> query = _db.Hotels;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Hotel>> GetAllAsync(Expression<Func<Hotel, bool>> filter = null)
        {
            IQueryable<Hotel> query = _db.Hotels;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Hotel entity)
        {
             _db.Hotels.Remove(entity);
            await SaveAsync();
            
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hotel entity)
        {
            _db.Hotels.Update(entity);
            await _db.SaveChangesAsync();
        }

    }
}
