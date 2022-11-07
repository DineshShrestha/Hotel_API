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
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            return Ok(await _db.Hotels.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async  Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var hotel =await _db.Hotels.FirstOrDefaultAsync(u=> u.Id == id);
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
        public async Task<ActionResult<HotelDTO>> CreateHotel([FromBody]HotelCreateDTO hotelDTO) {

            /*if (!ModelState.IsValid)
            {
                return BadRequest();
            }*/

            if(await _db.Hotels.FirstOrDefaultAsync(u=> u.Name.ToLower() == hotelDTO.Name.ToLower()) != null) {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }

            if(hotelDTO == null)
            {
                return BadRequest();
            }
            //if(hotelDTO.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            Hotel model = new()
            {
                Amenity = hotelDTO.Amenity,
                Details = hotelDTO.Details,
                ImageUrl = hotelDTO.ImageUrl,
                Name = hotelDTO.Name,
                Occupancy = hotelDTO.Occupancy,
                Rate = hotelDTO.Rate,
                Sqft = hotelDTO.Sqft,
            };
            await _db.Hotels.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetHotel",new { id = model.Id },  model);
        }

        [HttpDelete("{id:int}", Name = "DeleteHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id == 0)
            {
                return BadRequest();    
            }
            var hotel = await _db.Hotels.FirstOrDefaultAsync(u => u.Id == id);  
            if(hotel == null)
            {
                return NotFound();
            }

            _db.Hotels.Remove(hotel);
           await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Updatehotel(int id, [FromBody]HotelUpdateDTO hotelDTO)
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
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialhotel(int id, JsonPatchDocument<HotelUpdateDTO> patchDTO) { 
            if(patchDTO == null || id== 0)
            {
                return BadRequest();
            }
            var hotel = await _db.Hotels.AsNoTracking().FirstOrDefaultAsync(u=>u.Id == id);

           
            HotelUpdateDTO hotelDTO = new()
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
            await _db.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }    }
}
