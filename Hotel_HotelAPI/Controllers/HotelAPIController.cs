using Hotel_HotelAPI.Data;
using Hotel_HotelAPI.Logging;
using Hotel_HotelAPI.Models;
using Hotel_HotelAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace Hotel_HotelAPI.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {

        private readonly ILogging _logger;
        
        public HotelAPIController(ILogging logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HotelDTO>> GetHotels()
        {
            _logger.Log("Getting all hotels", "");
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
                _logger.Log("Get hotels error with ID" + id, "error");
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

        [HttpDelete("{id:int}", Name = "DeleteHotel")]
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

        [HttpPut("{id:int}", Name = "UpdateHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Updatehotel(int id, [FromBody]HotelDTO hotelDTO)
        {
            if (hotelDTO == null || id!=hotelDTO.Id)
            {
                return BadRequest();
            }
            var hotel = HotelStore.hotelList.FirstOrDefault(u => u.Id == id);
            hotel.Name = hotelDTO.Name;
            hotel.Sqft = hotelDTO.Sqft;
            hotel.Occupancy = hotelDTO.Occupancy;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialhotel(int id, JsonPatchDocument<HotelDTO> patchDTO) { 
            if(patchDTO == null || id== 0)
            {
                return BadRequest();
            }
            var hotel = HotelStore.hotelList.FirstOrDefault(u=>u.Id == id);

            if(hotel== null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(hotel, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
