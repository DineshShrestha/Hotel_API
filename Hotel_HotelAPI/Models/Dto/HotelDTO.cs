using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Hotel_HotelAPI.Models.Dto
{
    public class HotelDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Occupancy { get; set; }

        public int Sqft { get; set; }
    }
}
