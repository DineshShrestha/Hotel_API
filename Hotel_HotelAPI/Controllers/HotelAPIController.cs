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
        public ActionResult<IEnumerable<HotelDTO>> GetHotels()
        {
            return Ok(HotelStore.hotelList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<HotelDTO> GetHotel(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var hotel =HotelStore.hotelList.FirstOrDefault(u=> u.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }
    }
}
