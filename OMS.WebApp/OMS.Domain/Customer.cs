namespace OMS.Domain
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public int CustomerTypeId { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}