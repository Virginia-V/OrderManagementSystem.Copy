namespace OMS.Domain
{
    public class OrderType : Entity
    {
        public string Type { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
