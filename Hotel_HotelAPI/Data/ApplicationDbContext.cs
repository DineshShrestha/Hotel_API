using Hotel_HotelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_HotelAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 

        }
  
        public DbSet<Hotel> Hotels { get; set; }
    }
}
