namespace OMS.Domain
{
    public class CustomerType : Entity
    {
        public string Type { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
