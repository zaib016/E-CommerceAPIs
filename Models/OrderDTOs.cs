using System.ComponentModel.DataAnnotations.Schema;
using E_CommerceAPIs.Models.Entities;

namespace E_CommerceAPIs.Models
{
    public class OrderDTOs
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        //public User User { get; set; }
        public required string Price { get; set; }
        public required string Status { get; set; }
    }
}
