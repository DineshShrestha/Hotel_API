using Hotel_HotelAPI.Data;
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
            return HotelStore.hotelList;
        }

        [HttpGet("{id:int}")]
        public HotelDTO GetHotel(int id)
        {
            return HotelStore.hotelList.FirstOrDefault(u=> u.Id == id);
        }
    }
}
