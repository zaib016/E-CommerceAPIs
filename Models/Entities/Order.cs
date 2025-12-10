using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceAPIs.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        //public User User { get; set; }
        public required string Price { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
