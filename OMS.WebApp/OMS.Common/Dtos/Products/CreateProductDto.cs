using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Products
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(20)]
        public string ProductSKU { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [Range(0, 999.99)]
        public decimal ProductPrice { get; set; }
    }
}
