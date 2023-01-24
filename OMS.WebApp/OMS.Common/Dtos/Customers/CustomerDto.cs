namespace OMS.Common.Dtos.Customers
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CustomerType { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string Email { get; set; }
    }
}
