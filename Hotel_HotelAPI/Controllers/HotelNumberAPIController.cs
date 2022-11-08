using AutoMapper;
using Hotel_HotelAPI.Data;
using Hotel_HotelAPI.Models;
using Hotel_HotelAPI.Models.Dto;
using Hotel_HotelAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Hotel_HotelAPI.Controllers
{
    [Route("api/HotelNumberAPI")]
    [ApiController]
    public class HotelNumberAPIController : ControllerBase
    {

        protected APIResponse _response;
        private readonly IHotelNumberRepository _dbHotelNumber;
        private readonly IHotelRepository _dbHotel;
        private readonly IMapper _mapper;
        public HotelNumberAPIController(IHotelNumberRepository dbHotelNumber, IMapper mapper, IHotelRepository dbHotel)
        {
            _dbHotelNumber = dbHotelNumber;
            _mapper = mapper;
            this._response = new();
            _dbHotel = dbHotel;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetHotelNumbers()
        {
            try
            {

                IEnumerable<HotelNumber> hotelNumberList = await _dbHotelNumber.GetAllAsync();
                _response.Result = _mapper.Map<List<HotelNumberDTO>>(hotelNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetHotelNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetHotelNumber(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadGateway;
                    return BadRequest(_response);
                }
                var hotelNumber = await _dbHotelNumber.GetAsync(u => u.HotelNo == id);
                if (hotelNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<HotelNumberDTO>(hotelNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateHotelNumber([FromBody] HotelNumberCreateDTO createDTO)
        {

            try
            {


                if (await _dbHotelNumber.GetAsync(u => u.HotelNo == createDTO.HotelNo) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa Number already exists!");
                    return BadRequest(ModelState);
                }
                if(await _dbHotel.GetAsync(u=>u.Id == createDTO.HotelID)== null)
                {
                    ModelState.AddModelError("CustomError", "Villa Number already exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                HotelNumber hotelNumber = _mapper.Map<HotelNumber>(createDTO);

                await _dbHotelNumber.CreateAsync(hotelNumber);
                _response.Result = _mapper.Map<HotelNumberDTO>(hotelNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
                return CreatedAtRoute("GetHotel", new { id = hotelNumber.HotelNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpDelete("{id:int}", Name = "DeleteHotelNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteHotelNumber(int id)
        {
            try
            {

                if (id == 0)
                {
                    return BadRequest();
                }
                var hotelNumber = await _dbHotelNumber.GetAsync(u => u.HotelNo == id);
                if (hotelNumber == null)
                {
                    return NotFound();
                }

                await _dbHotelNumber.RemoveAsync(hotelNumber);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateHotelNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatehotelNumber(int id, [FromBody] HotelNumberUpdateDTO updateDTO)
        {
            try
            {

                if (updateDTO == null || id != updateDTO.HotelNo)
                {
                    return BadRequest();
                }
                if (await _dbHotel.GetAsync(u => u.Id == updateDTO.HotelID) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa Number already exists!");
                    return BadRequest(ModelState);
                }
                HotelNumber model = _mapper.Map<HotelNumber>(updateDTO);
                await _dbHotelNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialHotelNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialhotelNumber(int id, JsonPatchDocument<HotelNumberUpdateDTO> patchDTO)
        {

            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var hotelNumber = await _dbHotelNumber.GetAsync(u => u.HotelNo == id, tracked: false);


            HotelNumberUpdateDTO hotelNumberDTO = _mapper.Map<HotelNumberUpdateDTO>(hotelNumber);
            if (hotelNumber == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(hotelNumberDTO, ModelState);
            HotelNumber model = _mapper.Map<HotelNumber>(hotelNumberDTO);
            await _dbHotelNumber.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
