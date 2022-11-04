using Hotel_HotelAPI.Data;
using Hotel_HotelAPI.Models;
using Hotel_HotelAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_HotelAPI.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        
        
        public HotelAPIController(ApplicationDbContext db)
        {
          _db=db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HotelDTO>> GetHotels()
        {
            return Ok(_db.Hotels);
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
            var hotel =_db.Hotels.FirstOrDefault(u=> u.Id == id);
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

            if(_db.Hotels.FirstOrDefault(u=> u.Name.ToLower() == hotelDTO.Name.ToLower()) != null) {
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
            Hotel model = new()
            {
                Amenity = hotelDTO.Amenity,
                Details = hotelDTO.Details,
                Id = hotelDTO.Id,
                ImageUrl = hotelDTO.ImageUrl,
                Name = hotelDTO.Name,
                Occupancy = hotelDTO.Occupancy,
                Rate = hotelDTO.Rate,
                Sqft = hotelDTO.Sqft,
            };
            _db.Hotels.Add(model);
            _db.SaveChanges();
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
            var hotel = _db.Hotels.FirstOrDefault(u => u.Id == id);  
            if(hotel == null)
            {
                return NotFound();
            }

            _db.Hotels.Remove(hotel);
            _db.SaveChanges();
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
            //var hotel = HotelStore.hotelList.FirstOrDefault(u => u.Id == id);
            //hotel.Name = hotelDTO.Name;
            //hotel.Sqft = hotelDTO.Sqft;
            //hotel.Occupancy = hotelDTO.Occupancy;
            Hotel model = new()
            {
                Amenity = hotelDTO.Amenity,
                Details = hotelDTO.Details,
                Id = hotelDTO.Id,
                ImageUrl = hotelDTO.ImageUrl,
                Name = hotelDTO.Name,
                Occupancy = hotelDTO.Occupancy,
                Rate = hotelDTO.Rate,
                Sqft = hotelDTO.Sqft,
            };
            _db.Hotels.Update(model);
            _db.SaveChanges();
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
            var hotel = _db.Hotels.AsNoTracking().FirstOrDefault(u=>u.Id == id);

           
            HotelDTO hotelDTO = new()
            {
                Amenity = hotel.Amenity,
                Details = hotel.Details,
                Id = hotel.Id,
                ImageUrl = hotel.ImageUrl,
                Name = hotel.Name,
                Occupancy = hotel.Occupancy,
                Rate = hotel.Rate,
                Sqft = hotel.Sqft,
            };

            if (hotel == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(hotelDTO, ModelState);

            Hotel model = new()
            {
                Amenity = hotelDTO.Amenity,
                Details = hotelDTO.Details,
                Id = hotelDTO.Id,
                ImageUrl = hotelDTO.ImageUrl,
                Name = hotelDTO.Name,
                Occupancy = hotelDTO.Occupancy,
                Rate = hotelDTO.Rate,
                Sqft = hotelDTO.Sqft,
            };
            _db.Hotels.Update(model);
            _db.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }    }
}
