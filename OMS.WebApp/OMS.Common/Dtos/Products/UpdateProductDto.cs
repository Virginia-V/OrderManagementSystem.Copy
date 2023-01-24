using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Products
{
    public class UpdateProductDto
    {
        [StringLength(50)]
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        [Range(0, 999.99)]
        public decimal? ProductPrice { get; set; }

    }
}
