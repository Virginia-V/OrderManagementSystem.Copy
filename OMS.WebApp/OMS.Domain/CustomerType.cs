namespace OMS.Domain
{
    public class CustomerType : BaseEntity
    {
        public string Type { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
