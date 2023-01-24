using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Customers
{
    public class CreateCustomerDto
    {
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }
        [Required]
        public int CustomerTypeId { get; set; }
        [Required]
        [StringLength(100)]
        public string BillingAddress { get; set; }
        [Required]
        [StringLength(100)]
        public string ShippingAddress { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
