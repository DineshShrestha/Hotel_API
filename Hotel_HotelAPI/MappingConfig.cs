using AutoMapper;
using Hotel_HotelAPI.Models;
using Hotel_HotelAPI.Models.Dto;

namespace Hotel_HotelAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Hotel, HotelDTO>();
            CreateMap<HotelDTO, Hotel>();

            CreateMap<Hotel, HotelCreateDTO>().ReverseMap();
            CreateMap<Hotel, HotelUpdateDTO>().ReverseMap();

            CreateMap<HotelNumber, HotelNumberDTO>().ReverseMap();

            CreateMap<HotelNumber, HotelNumberCreateDTO>().ReverseMap();
            CreateMap<HotelNumber, HotelNumberUpdateDTO>().ReverseMap();
        }
        
    }
}
