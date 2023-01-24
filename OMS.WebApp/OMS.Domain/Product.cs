namespace OMS.Domain
{
    public class Product : BaseEntity
    {
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public decimal ProductPrice { get; set; }
        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }
    }

}
