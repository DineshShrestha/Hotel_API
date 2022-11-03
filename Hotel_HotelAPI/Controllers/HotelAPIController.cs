using Hotel_HotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hotel_HotelAPI.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel{Id=1, Name="Pool View" },
                new Hotel{Id=2, Name="Beach View"}
            };
        }
    }
}
