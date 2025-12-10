using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceAPIs.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
