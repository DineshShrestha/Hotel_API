using Hotel_HotelAPI.Models;
using Hotel_HotelAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
namespace Hotel_HotelAPI.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<HotelDTO> GetHotels()
        {
            return new List<HotelDTO>
            {
                new HotelDTO{Id=1, Name="Pool View" },
                new HotelDTO{Id=2, Name="Beach View"}
            };
        }
    }
}
