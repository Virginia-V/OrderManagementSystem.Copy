using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Customers
{
    public class UpdateCustomerDto
    {
        [StringLength(20)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string CompanyName { get; set; }
        public int? CustomerTypeId { get; set; }
        [StringLength(100)]
        public string BillingAddress { get; set; }
        [StringLength(100)]
        public string ShippingAddress { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
