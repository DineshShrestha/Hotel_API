using Hotel_HotelAPI.Models.Dto;

namespace Hotel_HotelAPI.Data
{
    public static class HotelStore
    {
        public static List<HotelDTO> hotelList = new List<HotelDTO>
            {
                new HotelDTO{Id=1, Name="Pool View", Sqft=199, Occupancy=4 },
                new HotelDTO{Id=2, Name="Beach View", Sqft=599, Occupancy=5}
            };
    }
}
