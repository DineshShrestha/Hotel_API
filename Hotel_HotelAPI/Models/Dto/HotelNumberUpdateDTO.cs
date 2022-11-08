﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Hotel_HotelAPI.Models.Dto
{
    public class HotelNumberUpdateDTO
    {
        [Required]
        public int HotelNo { get; set; }

        public string SpecialDetails { get; set; }
    }
}
