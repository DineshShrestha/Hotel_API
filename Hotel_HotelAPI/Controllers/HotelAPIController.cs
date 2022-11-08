using AutoMapper;
using Hotel_HotelAPI.Data;
using Hotel_HotelAPI.Models;
using Hotel_HotelAPI.Models.Dto;
using Hotel_HotelAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_HotelAPI.Controllers
{
    [Route("api/HotelAPI")]
    [ApiController]
    public class HotelAPIController : ControllerBase 
    {

        private readonly IHotelRepository _dbHotel;

        private readonly IMapper _mapper;
        public HotelAPIController(IHotelRepository dbHotel, IMapper mapper)
        {
          _dbHotel=dbHotel;
            _mapper=mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            IEnumerable<Hotel> hotelList = await _dbHotel.GetAllAsync();
            return Ok(_mapper.Map<List<HotelDTO>>(hotelList));
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
            var hotel =await _dbHotel.GetAsync(u=> u.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<HotelDTO>(hotel));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelDTO>> CreateHotel([FromBody]HotelCreateDTO createDTO) {

    

            if(await _dbHotel.GetAsync(u=> u.Name.ToLower() == createDTO.Name.ToLower()) != null) {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }

            if(createDTO == null)
            {
                return BadRequest(createDTO);
            }

            Hotel model = _mapper.Map<Hotel>(createDTO);

            await _dbHotel.CreateAsync(model);    
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
            var hotel = await _dbHotel.GetAsync(u => u.Id == id);  
            if(hotel == null)
            {
                return NotFound();
            }

            await _dbHotel.RemoveAsync(hotel);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateHotel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Updatehotel(int id, [FromBody]HotelUpdateDTO updateDTO)
        {
            if (updateDTO == null || id!= updateDTO.Id)
            {
                return BadRequest();
            }
            Hotel model = _mapper.Map<Hotel>(updateDTO);
            await _dbHotel.UpdateAsync(model);
         
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
            var hotel = await _dbHotel.GetAsync(u=>u.Id == id, tracked:false);


            HotelUpdateDTO hotelDTO = _mapper.Map<HotelUpdateDTO>(hotel);
            if (hotel == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(hotelDTO, ModelState);
            Hotel model = _mapper.Map<Hotel>(hotelDTO);
            await _dbHotel.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }    }
}
