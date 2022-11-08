using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_HotelAPI.Models
{
    public class HotelNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HotelNo { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
