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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HotelDTO>> GetHotels()
        {
            return Ok(HotelStore.hotelList);
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HotelDTO> CreateHotel([FromBody]HotelDTO hotelDTO) {

            /*if (!ModelState.IsValid)
            {
                return BadRequest();
            }*/

            if(HotelStore.hotelList.FirstOrDefault(u=> u.Name.ToLower() == hotelDTO.Name.ToLower()) != null) {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }

            if(hotelDTO == null)
            {
                return BadRequest();
            }
            if(hotelDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            hotelDTO.Id = HotelStore.hotelList.OrderByDescending(u=>u.Id).FirstOrDefault().Id + 1;
            HotelStore.hotelList.Add(hotelDTO);

            return CreatedAtRoute("GetHotel",new { id = hotelDTO.Id },  hotelDTO);
        }

        [HttpDelete("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteHotel(int id)
        {
            if (id == 0)
            {
                return BadRequest();    
            }
            var hotel = HotelStore.hotelList.FirstOrDefault(u => u.Id == id);  
            if(hotel == null)
            {
                return NotFound();
            }

            HotelStore.hotelList.Remove(hotel);
            return NoContent();
        }
    }
}
