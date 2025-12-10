using System.ComponentModel.DataAnnotations.Schema;
using E_CommerceAPIs.Models.Entities;

namespace E_CommerceAPIs.Models
{
    public class ProductDTOs
    {
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("CategoryId")]
        public int  CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
