using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Orders
{
    public class CreateOrderDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderedAt { get; set; }
        [Required]
        [StringLength(15)]
        public string PurchaseOrderNumber { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int OrderTypeId { get; set; }
        [Required]
        public IList<OrderedProductDto> OrderedProducts { get; set; }
    }
}
